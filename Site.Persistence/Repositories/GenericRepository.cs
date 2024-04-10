using Site.Application.Definitions.Contracts.Repositories;
using Site.Persistence.Contexts.ApplicationContext;
using System.Linq.Expressions;

namespace Site.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().LongCountAsync(predicate);
        }
        public async Task<bool> Exist(int id)
        {
            var entity = await GetById(id);
            return entity != null;
        }

        public async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
        public async Task<TEntity> GetLast(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().LastOrDefaultAsync(predicate);
        }
        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<int> ids)
        {
            var query = _context.Set<TEntity>().Where(e => ids.Contains(e.GetHashCode()));
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<IReadOnlyList<TEntity>> GetAll<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool ascending = true)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllIncluding<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool ascending = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAll<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool ascending = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            return await query.ToListAsync();
        }


        public async Task<IEnumerable<TEntity>> GetPaged(int pageNumber, int pageSize)
        {
            return await _context.Set<TEntity>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetFilteredAndPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
        {
            var query = _context.Set<TEntity>().Where(predicate);

            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetFilteredAndPaged<TKey>(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, Expression<Func<TEntity, TKey>> orderBy, bool ascending = true)
        {
            var query = _context.Set<TEntity>().Where(predicate);
            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetFiltered(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetFiltered<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, bool ascending = true)
        {
            var query = _context.Set<TEntity>().Where(predicate);
            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            return await query.ToListAsync();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

    }
}
