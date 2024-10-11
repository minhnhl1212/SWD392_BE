namespace NShop.src.Application.DTOs.Users;

public class UserCreateDto
{
  public string FullName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string? Phone { get; set; }
  public string? Status { get; set; }

}