using NShop.src.Application.DTOs.Pagination;

namespace NShop.src.Application.DTOs.Supplier
{
    public class SupplierQuery : PaginationReq
    {
        public string? SearchKeyword { get; set; }
    }


 
}
