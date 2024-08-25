using eAppointment.Backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace eAppointment.Backend.Domain.Abstractions
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _appDbContext.Set<TEntity>();

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? expression = null,
            bool trackChanges = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _appDbContext.Set<TEntity>();

            if (include != null)
            {
                query = include(query);
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Tuple<int, int, List<TEntity>>> GetAllAsync(
            int page, int pageSize,
            Expression<Func<TEntity, bool>>? expression = null,
            bool trackChanges = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _appDbContext.Set<TEntity>();

            if (include != null)
            {
                query = include(query);
            }

            int totalCount = query.Count();
            int filteredCount = totalCount;

            if (expression != null)
            {
                query = query.Where(expression);
                filteredCount = query.Count();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            var entity = await query.ToListAsync(cancellationToken);

            return new Tuple<int, int, List<TEntity>>(totalCount, filteredCount, entity);
        }

        public async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>>? expression = null,
            bool trackChanges = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _appDbContext.Set<TEntity>();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public TEntity Get(
            Expression<Func<TEntity, bool>>? expression = null,
            bool trackChanges = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            IQueryable<TEntity> query = _appDbContext.Set<TEntity>();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefault();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task AddRangeAsync(
            IEnumerable<TEntity> entities, 
            CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> expression, 
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(expression, cancellationToken);
        }

        public bool Any(
            Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Any(expression);
        }
    }
}
