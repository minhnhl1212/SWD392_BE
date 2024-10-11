namespace NShop.src.Domain.Users;

using NShop.src.Domain.Role;
using NShop.src.Domain.Store;
using Org.BouncyCastle.Bcpg.OpenPgp;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Status { get; set; }
    public Guid RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}


  public Role Role { get; set; } = null!;


  public ICollection<Store> Stores { get; set; } = [];
}