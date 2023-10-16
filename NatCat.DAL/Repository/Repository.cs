using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.DataGroup;
using NatCat.Model.Exception;

namespace NatCat.DAL.Repository
{
    public class Repository<TEntity, TDetailDto, TListDto> : IRepository<TEntity, TDetailDto, TListDto> where TEntity : BaseGuidEntity 
    {
        private readonly IMapper _mapper;
        private readonly NatCatDbContext _dbContext;

        public Repository(NatCatDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async virtual Task<Guid> AddAsync(TEntity source)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(source);

                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity.Id;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async virtual Task DeleteAsync(Guid id)
        {
            var entity = await FindAsync(id);
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async virtual Task SoftDeleteAsync(Guid id) 
        {
            var entity = await FindAsync(id);
            entity.IsDeleted = true;
            _dbContext.Update(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<bool> Exists(Guid id)
        {
            var entity = await FindAsync(id);
            return entity != null;
        }

        public async virtual Task<PagedResult<TListDto>> PagedAsync(
            QueryParameters<TEntity> queryParameters,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var baseSet = _dbContext.Set<TEntity>();

            foreach(var include in includes)
            {
                baseSet.Include(include);
            }

            var totalSize = await baseSet.Where(queryParameters.wc).CountAsync();

            var items = await _dbContext.Set<TEntity>()
                                .Where(NotSoftDeleted)
                                .Where(queryParameters.wc)
                                .Skip(queryParameters.PageNumber)
                                .Take(queryParameters.PageSize)
                                .ProjectTo<TListDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();

            return new PagedResult<TListDto>
            {
                PageNumber = queryParameters.PageNumber,
                RecordNumber = queryParameters.PageSize,
                TotalCount = totalSize,
                Items = items
            };
        }

        public async virtual Task UpdateAsync(Guid id, TEntity source)
        {
            if (id != source.Id)
            {
                throw new BadRequestException("Invalid Id used in request");
            }

            var entity = await FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(typeof(TEntity).Name, id);
            }

            _mapper.Map(source, entity);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TListDto>> ListAllAsync(
            params Expression<Func<TEntity, object>>[] includes
            )
        {
            var baseSet = _dbContext.Set<TEntity>();

            foreach (var inc in includes)
            {
                baseSet.Include(inc);
            }
            return await baseSet
                               .Where(NotSoftDeleted)
                               .ProjectTo<TListDto>(_mapper.ConfigurationProvider)
                               .ToListAsync();
        }

        public async Task<IEnumerable<TListDto>> ListAllAsync(
            Expression<Func<TEntity, bool>> wc,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var baseSet = _dbContext.Set<TEntity>();

            foreach (var inc in includes)
            {
                baseSet.Include(inc);
            }

            return await baseSet
                               .Where(wc)
                               .Where(NotSoftDeleted)
                               .ProjectTo<TListDto>(_mapper.ConfigurationProvider)
                               .ToListAsync();
        }

        public async virtual Task<TDetailDto> GetAsync(Guid? id)
        {
            var result = await FindAsync(id);

            if (result is null)
            {
                throw new NotFoundException(typeof(TEntity).Name, "Not found");
            }

            return _mapper.Map<TDetailDto>(result);
        }

        private async Task<TEntity?> FindAsync(Guid? id)
        {
            var set = await _dbContext.Set<TEntity>()
                                      .Where(NotSoftDeleted)
                                      .Where(x => x.Id == id)
                                      .ToListAsync();
            if (set.Any()) {
                return set.First();
            }
            else {
                return null;
            }
        }

        public async Task<PagedResult<TListDto>> PagedOrderAsync(
            QueryParameters<TEntity> queryParameters, 
            Expression<Func<TEntity, object>> orderBy, 
            bool orderByDescending = false,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var baseSet = _dbContext.Set<TEntity>();

            foreach(var include in includes)
            {
                baseSet.Include(include);
            }

            var wc = queryParameters != null ? queryParameters.wc : x => true;
            var pageNumber = queryParameters != null ? queryParameters.PageNumber : 0;
            var pageSize = queryParameters != null ? queryParameters.PageSize : 10;

            var totalSize = await baseSet.Where(wc).CountAsync();

            var items = _dbContext.Set<TEntity>()
                                .Where(NotSoftDeleted)
                                .Where(wc);

            if (orderBy != null)
            {
                items = orderByDescending ?
                    items.OrderByDescending(orderBy) :
                    items.OrderBy(orderBy);
            }
            
            var retItems = await items
                                .Skip(pageNumber)
                                .Take(pageSize)
                                .ProjectTo<TListDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();

            return new PagedResult<TListDto>
            {
                PageNumber = pageNumber,
                RecordNumber = pageSize,
                TotalCount = totalSize,
                Items = retItems
            };
        }

        public async Task<IEnumerable<TListDto>> ListAllAsync(Expression<Func<TEntity, bool>> wc, 
        Expression<Func<TEntity, object>> orderBy,
        bool orderByDescending = false,
        params Expression<Func<TEntity, object>>[] includes)
        {
            var baseSet = _dbContext.Set<TEntity>();

            foreach (var inc in includes)
            {
                baseSet.Include(inc);
            }

            var items = baseSet
                        .Where(wc)
                        .Where(NotSoftDeleted);
            items = orderByDescending ? 
                        items.OrderByDescending(orderBy) :
                        items.OrderBy(orderBy);
            return await items
                               .ProjectTo<TListDto>(_mapper.ConfigurationProvider)
                               .ToListAsync();
        }

        public async Task<TEntity> GetEntityAsync(Guid? id, params Expression<Func<TEntity, object>>[] includes)
        {
            var baseSet = _dbContext.Set<TEntity>();
            foreach(var inc in includes)
            {
                baseSet.Include(inc);
            }
            var result = baseSet.FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                throw new NotFoundException(typeof(TEntity).Name, "Not found");
            }

            return result;
        }

        public async Task<IEnumerable<TEntity>> EntityListAllAsync
        (Expression<Func<TEntity, bool>> wc, 
        Expression<Func<TEntity, object>> orderBy, 
        bool orderByDescending = false,
        params Expression<Func<TEntity, 
        object>>[] includes)
        {
            var baseSet = _dbContext.Set<TEntity>();

            foreach (var inc in includes)
            {
                baseSet.Include(inc);
            }

            var items = baseSet
                        .Where(wc)
                        .Where(NotSoftDeleted);
            items = orderByDescending ? 
                        items.OrderByDescending(orderBy) :
                        items.OrderBy(orderBy);
            return await items
                               .ToListAsync();
        }

        private Expression<Func<TEntity, bool>> NotSoftDeleted => x => !x.IsDeleted;




    }
}

