using NShop.src.Application.DTOs.Pagination;

namespace NShop.src.Application.DTOs.Product
{
    public class ProductQuery : PaginationReq
    {
        public string? SearchKeyword { get; set; }

        public string Categories { get; set; } = null!;
    }
}
