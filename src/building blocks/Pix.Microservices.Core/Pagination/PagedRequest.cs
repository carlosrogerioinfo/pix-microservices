using System.ComponentModel.DataAnnotations;

namespace Pix.Microservices.Core.Pagination;

public class PagedRequest
{
    private int _page = 1;
    private int _pageSize = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Page must be >= 1")]
    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100")]
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 1 : value > 100 ? 100 : value;
    }
}
