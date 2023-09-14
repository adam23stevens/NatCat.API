using System;
using System.Linq.Expressions;
using NatCat.DAL.Entity;
using NatCat.Model.DataGroup;

namespace NatCat.DAL.Contracts
{
    public interface IRepository<TEntity, TDetailDto, TListDto>
    {
        Task<Guid> AddAsync(TEntity source);
        Task<PagedResult<TListDto>> PagedAsync(
            QueryParameters<TEntity> queryParameters,
            params Expression<Func<TEntity, object>>[] includes
            );
        Task<PagedResult<TListDto>> PagedAsync(
        QueryParameters<TEntity>? queryParameters = null,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool orderByDescending = false,
        params Expression<Func<TEntity, object>>[] includes
        );
        Task<IEnumerable<TListDto>> ListAllAsync(
            params Expression<Func<TEntity, object>>[] includes
            );
        Task<IEnumerable<TListDto>> ListAllAsync(
            Expression<Func<TEntity, bool>> wc,
            params Expression<Func<TEntity, object>>[] includes
            );
        Task<IEnumerable<TListDto>> ListAllAsync(
        Expression<Func<TEntity, bool>> wc,
        Expression<Func<TEntity, object>> orderBy,
        bool orderByDescending = false,
        params Expression<Func<TEntity, object>>[] includes
        );
        Task<IEnumerable<TEntity>> EntityListAllAsync(
        Expression<Func<TEntity, bool>> wc,
        Expression<Func<TEntity, object>> orderBy,
        bool orderByDescending = false,
        params Expression<Func<TEntity, object>>[] includes
        );
        Task<TDetailDto> GetAsync(Guid? id);
        Task<TEntity> GetEntityAsync(Guid? id, params Expression<Func<TEntity, object>>[] includes);
        Task UpdateAsync(Guid id, TEntity entity);
        Task DeleteAsync(Guid id);
        Task SoftDeleteAsync(Guid id);
        Task<bool> Exists(Guid id);
    }
}

