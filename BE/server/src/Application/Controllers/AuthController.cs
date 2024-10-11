namespace NShop.src.Application.Controllers;
using NShop.src.Application.Shared.Type;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Database;
using NShop.src.Application.Services;
using NShop.src.Application.DTOs.Auth;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase

{
  private readonly ILogger<AuthController> _logger;

  private readonly IAuthService _authService;

  public AuthController(ILogger<AuthController> logger, NShopDbContext dbContext, IAuthService authService)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    _authService = authService;
  }

  [HttpGet("login")]
  public IActionResult Login([FromQuery] string redirect)
  {
    var props = new AuthenticationProperties { RedirectUri = "api/v1/auth/signin-google?rdc=" + redirect };
    return Challenge(props, GoogleDefaults.AuthenticationScheme);
  }

  [HttpGet("signin-google")]
  public async Task<IActionResult> GoogleLogin([FromQuery] string rdc)
  {
    _logger.LogInformation("Google Login");
    // Get data from google
    var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    if (response.Principal == null) return ErrorResp.BadRequest("No principal found");

    var name = response.Principal.FindFirstValue(ClaimTypes.Name);
    var fullName = response.Principal.FindFirstValue(ClaimTypes.GivenName);
    var email = response.Principal.FindFirstValue(ClaimTypes.Email);

    if (string.IsNullOrEmpty(email))
    {
      return ErrorResp.BadRequest("Email is required");
    }

    var resp = await _authService.HandleGoogleLogin(rdc, new GgAuthInfo
    {
      Email = email,
      FullName = fullName ?? name,
    });

    if (resp.Success && resp.RedirectLink != null)
    {
      return Redirect(resp.RedirectLink);
    }
    else
    {
      return ErrorResp.BadRequest(resp.Message ?? "Login failed");
    }
  }


  [HttpGet("test")]
  public IActionResult Test([FromQuery] TokenResp req)
  {
    _logger.LogInformation("Test");
    return Ok(req);
  }
}