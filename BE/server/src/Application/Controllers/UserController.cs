namespace NShop.src.Application.Controllers;

using NShop.src.Application.DTOs.User;
using NShop.src.Application.DTOs.Users;
using NShop.src.Application.Service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/users")]
public class UserController : ControllerBase
{

  private readonly ILogger<UserController> _logger;
  private readonly IUserService _service;

  public UserController(ILogger<UserController> logger, IUserService service)
  {
    _logger = logger;
    _service = service;
  }

  [HttpGet("")]
  public async Task<IActionResult> Get([FromQuery] UserQuery query)
  {
    _logger.LogInformation("Get all users request received");

    return await _service.HandleGetAllAsync(query);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(Guid id)
  {
    _logger.LogInformation("Get user by id request received");

    return await _service.HandleGetByIdAsync(id);
  }

  [HttpPost("")]
  public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
  {
    _logger.LogInformation("Create user request received");

    return await _service.HandleCreateAsync(dto);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto dto)
  {
    _logger.LogInformation("Update user request received");

    return await _service.HandleUpdateAsync(id, dto);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    _logger.LogInformation("Delete user request received");

    return await _service.HandleDeleteAsync(id);
  }
}
