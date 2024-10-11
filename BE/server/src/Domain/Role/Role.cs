namespace NShop.src.Domain.Role;

public class Role
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public string? Status { get; set; }
}