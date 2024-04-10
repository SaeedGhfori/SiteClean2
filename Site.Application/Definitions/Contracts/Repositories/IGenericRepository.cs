using System.Linq.Expressions;

namespace Site.Application.Definitions.Contracts.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<long> Count(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Exist(int id);

        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetLast(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetByIds(IEnumerable<int> ids);

        Task<IEnumerable<TEntity>> GetAll<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool ascending = true, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IReadOnlyList<TEntity>> GetAll();
        Task<IReadOnlyList<TEntity>> GetAll<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool ascending = true);
        Task<IEnumerable<TEntity>> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetAllIncluding<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool ascending = true, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> GetPaged(int pageNumber, int pageSize);
        Task<IEnumerable<TEntity>> GetFilteredAndPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
        Task<IEnumerable<TEntity>> GetFilteredAndPaged<TKey>(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, Expression<Func<TEntity, TKey>> orderBy, bool ascending = true);
        Task<IEnumerable<TEntity>> GetFiltered(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetFiltered<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, bool ascending = true);

        Task<TEntity> Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);

        Task Remove(TEntity entity);
        Task RemoveRange(IEnumerable<TEntity> entities);

        Task Update(TEntity entity);
        Task UpdateRange(IEnumerable<TEntity> entities);

    }
}
