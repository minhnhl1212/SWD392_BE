using NShop.src.Application.DTOs.Pagination;

namespace NShop.src.Application.DTOs.Review
{
    public class ReviewQuery : PaginationReq
    {
        public string? SearchKeyword { get; set; }
    }
}
