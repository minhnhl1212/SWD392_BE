using NShop.src.Application.Shared.Type;
using NShop.src.Domain.Users;
using NShop.src.Application.Shared.Type;
namespace NShop.src.Domain.Store;

public class Store : BaseEntity
{
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public string? Location { get; set; }
  public decimal Rating { get; set; }
  public string? Status { get; set; }

  public ICollection<User> Users { get; set; } = [];
}