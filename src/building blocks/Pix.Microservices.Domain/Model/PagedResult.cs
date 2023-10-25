namespace Pix.Microservices.Domain.Model
{
    public abstract class PagedResultBase
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;
    }

    public class PagedResult : PagedResultBase
    {
    }

    public class PagedResponse<T, P>
    {
        public PagedResponse(IEnumerable<T> data, P paging)
        {
            Data = data;
            Paging = paging;
        }

        public bool Success { get; set; }
        public IEnumerable<T> Data { get; set; }
        public P Paging { get; set; }
    }
}
