namespace Pix.Microservices.Domain.Model
{
    public class PaginationFilter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PaginationFilter()
        {
            Page = 1;
            PageSize = 10;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            Page = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
