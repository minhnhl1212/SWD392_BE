namespace NShop.src.Application.Controllers;

using NShop.src.Application.Shared.Type;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1")]
public class PingController : ControllerBase
{

    private readonly ILogger<PingController> _logger;

    public PingController(ILogger<PingController> logger)
    {
        _logger = logger;
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        _logger.LogInformation("Ping request received");

        var resp = new BaseResp { Code = 200, Message = "pong" };

        return Ok(resp);
    }
}
