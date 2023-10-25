using AspNetCore.IQueryable.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pix.Microservices.Domain.Extention;
using Pix.Microservices.Domain.Model;
using Pix.Microservices.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace Pix.Microservices.Infrastructure.Repositories.Base
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbSet<T> _dbSet;
        private DbContext _context;

        public GenericRepository(DbContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int? skip = null, int? take = null)
        {
            if (skip.HasValue & take.HasValue)
            {
                return await _dbSet
                    .AsNoTrackingWithIdentityResolution()
                    .Skip(take.Value * (skip.Value - 1))
                    .Take(take.Value)
                    .ToListAsync();
            }

            return await _dbSet
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetDataAsync(
            Expression<Func<T, bool>> expression = null,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbSet
                .AsQueryable()
                .AsNoTrackingWithIdentityResolution();

            if (expression is not null)
                entity = entity.Where(expression);

            foreach (var include in includes)
                entity = entity.Include(include);

            return await entity.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetListDataAsync(
            Expression<Func<T, bool>> expression = null,
            PaginationFilter paginationFilter = null,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbSet
                .AsQueryable()
                .AsNoTrackingWithIdentityResolution();

            if (expression is not null)
                entity = entity.Where(expression);

            foreach (var include in includes)
                entity = entity.Include(include);

            entity = entity.Skip(paginationFilter.PageSize * (paginationFilter.Page - 1));
            entity = entity.Take(paginationFilter.PageSize);

            return await entity.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetListDataAsync<TFilter>(
            TFilter filter,
            PaginationFilter paginationFilter,
            params Expression<Func<T, object>>[] includes) where TFilter : ICustomQueryable
        {
            var entity = _dbSet
                .AsNoTrackingWithIdentityResolution()
                .AsQueryable();

            foreach (var include in includes)
                entity = entity.Include(include);

            entity = entity.Skip(paginationFilter.PageSize * (paginationFilter.Page - 1));
            entity = entity.Take(paginationFilter.PageSize);

            return await entity.Apply(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetListDataAsync(
            Expression<Func<T, bool>> expression = null,
            PaginationFilter paginationFilter = null)
        {
            var entity = _dbSet
                .AsQueryable()
                .AsNoTrackingWithIdentityResolution();

            if (expression is not null)
                entity = entity.Where(expression);

            entity = entity.Skip(paginationFilter.PageSize * (paginationFilter.Page - 1));
            entity = entity.Take(paginationFilter.PageSize);

            return await entity.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetListDataAsync<TFilter>(
            TFilter filter,
            PaginationFilter paginationFilter) where TFilter : ICustomQueryable
        {
            var entity = _dbSet
                .AsNoTrackingWithIdentityResolution()
                .AsQueryable();

            entity = entity.Skip(paginationFilter.PageSize * (paginationFilter.Page - 1));
            entity = entity.Take(paginationFilter.PageSize);

            return await entity.Apply(filter).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            _context.SaveChanges();
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTrackingWithIdentityResolution().CountAsync(predicate);
        }

        public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTrackingWithIdentityResolution().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> Search<TFilter>(TFilter filter) where TFilter : ICustomQueryable
        {
            return await _dbSet.AsNoTrackingWithIdentityResolution().AsQueryable().Apply(filter).ToListAsync();
        }

        public async Task<PagedResponse<T, PagedResult>> SearchPaged<TFilter>(TFilter filter, PaginationFilter paginationFilter) where TFilter : ICustomQueryable
        {
            var queryable = await _dbSet.GetPagedAsync(paginationFilter.Page, paginationFilter.PageSize);

            var entity = await _dbSet
                .AsNoTrackingWithIdentityResolution()
                .AsQueryable()
                .Skip((paginationFilter.Page - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .Apply(filter).ToListAsync();

            return new PagedResponse<T, PagedResult>(entity, queryable);
        }

        public async Task<PagedResponse<T, PagedResult>> SearchPaged<TFilter>(TFilter filter, PaginationFilter paginationFilter, params Expression<Func<T, object>>[] includes) where TFilter : ICustomQueryable
        {
            var queryable = await _dbSet.GetPagedAsync(paginationFilter.Page, paginationFilter.PageSize);

            var entity = _dbSet
                .AsNoTrackingWithIdentityResolution()
                .AsQueryable()
                .Skip((paginationFilter.Page - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .Apply(filter);

            foreach (var include in includes)
                entity = entity.Include(include);

            return new PagedResponse<T, PagedResult>(await entity.ToListAsync(), queryable);
        }

        public async Task UpdateAsync(T entity, bool modifySingleEntity = false)
        {
            if (modifySingleEntity)
            {
                EntityEntry entityEntry = _context.Entry<T>(entity);
                entityEntry.State = EntityState.Modified;
            }
            else
            {
                await Task.FromResult(_dbSet.Update(entity));
            }

            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);

            _context.SaveChanges();
        }
    }

}
