namespace NShop.src.Application.Shared.Type;

using NShop.src.Application.Shared.Constant;
using Microsoft.AspNetCore.Mvc;

public static class ErrorResp
{
  public static IActionResult InternalServerError(string? message)
  {
    return new JsonResult(new { Error = message ?? RespMsg.INTERNAL_SERVER_ERROR }) { StatusCode = RespCode.INTERNAL_SERVER_ERROR };
  }

  public static IActionResult NotFound(string? message)
  {
    return new JsonResult(new { Error = message ?? RespMsg.NOT_FOUND }) { StatusCode = RespCode.NOT_FOUND };
  }

  public static IActionResult BadRequest(string? message)
  {
    return new JsonResult(new { Error = message ?? RespMsg.BAD_REQUEST }) { StatusCode = RespCode.BAD_REQUEST };
  }
  public static IActionResult Unauthorized(string? message)
  {
    return new JsonResult(new { Error = message ?? RespMsg.UNAUTHORIZED }) { StatusCode = RespCode.UNAUTHORIZED };
  }

  public static IActionResult Forbidden(string? message)
  {
    return new JsonResult(new { Error = message ?? RespMsg.FORBIDDEN }) { StatusCode = RespCode.FORBIDDEN };
  }

}
