using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Exception;

namespace NatCat.DAL.Repository
{
    public class SimpleRepository<TEntity> : ISimpleRepository<TEntity> where TEntity : BaseGuidEntity
    {
        private readonly NatCatDbContext _dbContext;
        private readonly IMapper _mapper;

        public SimpleRepository(NatCatDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TEntity> GetAsync(Guid? id)
        {
            var result = await FindAsync(id);

            if (result is null)
            {
                throw new NotFoundException(typeof(TEntity).Name, "Not found");
            }

            return result;
        }

        public async Task UpdateAsync(Guid id, TEntity source)
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

        public async Task SoftDeleteAsync(Guid id)
        {
            var entity = await FindAsync(id);
            entity.IsDeleted = true;
            _dbContext.Update(entity);

            await _dbContext.SaveChangesAsync();
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
            catch (Exception ex)
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

        public async virtual Task<bool> Exists(Guid id)
        {
            var entity = await FindAsync(id);
            return entity != null;
        }

        private async Task<TEntity?> FindAsync(Guid? id)
        {
            var set = await _dbContext.Set<TEntity>()
                                      .Where(NotSoftDeleted)
                                      .Where(x => x.Id == id)
                                      .ToListAsync();
            if (set.Any())
            {
                return set.First();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>> ListEntityAsync()
        {
            return await _dbContext.Set<TEntity>().Where(NotSoftDeleted).ToListAsync();
        }

        private Expression<Func<TEntity, bool>> NotSoftDeleted => x => !x.IsDeleted;
    }
}

