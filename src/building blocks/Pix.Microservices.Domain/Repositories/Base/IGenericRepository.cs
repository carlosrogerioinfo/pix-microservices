using AspNetCore.IQueryable.Extensions;
using Pix.Microservices.Domain.Model;
using System.Linq.Expressions;

namespace Pix.Microservices.Domain.Repositories.Base
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(int? skip = null, int? take = null);

        Task<T> GetByIdAsync(Guid id);

        Task<T> GetByIdAsync(int id);

        Task<T> GetDataAsync(
            Expression<Func<T, bool>> expression = null,
            params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetListDataAsync(
            Expression<Func<T, bool>> expression = null,
            PaginationFilter paginationFilter = null,
            params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetListDataAsync<TFilter>(
            TFilter filter,
            PaginationFilter paginationFilter,
            params Expression<Func<T, object>>[] includes) where TFilter : ICustomQueryable;

        Task<IEnumerable<T>> GetListDataAsync(
            Expression<Func<T, bool>> expression = null,
            PaginationFilter paginationFilter = null);

        Task<IEnumerable<T>> GetListDataAsync<TFilter>(
            TFilter filter,
            PaginationFilter paginationFilter) where TFilter : ICustomQueryable;

        Task AddAsync(T entity);

        Task<int> Count(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> Search<TFilter>(TFilter filter) where TFilter : ICustomQueryable;

        Task<PagedResponse<T, PagedResult>> SearchPaged<TFilter>(TFilter filter, PaginationFilter paginationFilter) where TFilter : ICustomQueryable;

        Task<PagedResponse<T, PagedResult>> SearchPaged<TFilter>(TFilter filter, PaginationFilter paginationFilter, params Expression<Func<T, object>>[] includes) where TFilter : ICustomQueryable;

        Task UpdateAsync(T entity, bool modifySingleEntity = false);

        void Delete(T entity);
    }
}
