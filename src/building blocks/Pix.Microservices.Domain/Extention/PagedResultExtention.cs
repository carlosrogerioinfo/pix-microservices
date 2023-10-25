using Microsoft.EntityFrameworkCore;
using Pix.Microservices.Domain.Model;

namespace Pix.Microservices.Domain.Extention
{
    public static class PagedResultExtention
    {
        public static async Task<PagedResult> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            return new PagedResult
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = await query.CountAsync()
            };
        }
    }
}
