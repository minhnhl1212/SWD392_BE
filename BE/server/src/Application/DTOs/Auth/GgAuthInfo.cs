namespace NShop.src.Application.DTOs.Auth;
using NShop.src.Application.Shared.Type;


public class GgAuthInfo
{
  public string Email { get; set; } = null!;
  public string? FullName { get; set; } = null!;
  public string? ProfileUrl { get; set; }
}

public class GgAuthResp
{
  public bool Success { get; set; }
  public string? Message { get; set; }
  public string? RedirectLink { get; set; }
}