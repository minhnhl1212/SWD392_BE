using NShop.src.Application.DTOs.Pagination;

namespace NShop.src.Application.DTOs.User;

public class UserQuery : PaginationReq
{
  public string? SearchKeyword { get; set; }
}