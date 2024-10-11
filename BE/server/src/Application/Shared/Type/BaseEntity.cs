namespace NShop.src.Application.Shared.Type;

using NShop.src.Domain.Users;

public class BaseEntity
{
  public Guid Id { get; set; }

  public string? Metadata { get; set; }
  public Guid CreatedBy { get; set; }
  public Guid UpdatedBy { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public User CreatedUser { get; set; } = null!;
  public User UpdatedUser { get; set; } = null!;
}