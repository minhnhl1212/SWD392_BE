namespace NShop.src.Application.Shared.Type;

using NShop.src.Application.Shared.Constant;
using Microsoft.AspNetCore.Mvc;

public static class SuccessResp
{
  public static IActionResult Ok(string? message)
  {
    return new JsonResult(new { Message = message ?? RespMsg.OK }) { StatusCode = RespCode.OK };
  }

  public static IActionResult Ok(object? data)
  {
    return new JsonResult(data) { StatusCode = RespCode.OK };
  }

  public static IActionResult Created(string? message)
  {
    return new JsonResult(new { Message = message ?? RespMsg.CREATED }) { StatusCode = RespCode.CREATED };
  }

  public static IActionResult Created(object? data)
  {
    return new JsonResult(data) { StatusCode = RespCode.CREATED };
  }

  public static IActionResult NoContent()
  {
    return new JsonResult(new { }) { StatusCode = RespCode.NO_CONTENT };
  }

  public static IActionResult Redirect(string url)
  {
    return new RedirectResult(url, false);
  }

  public static IActionResult Content(string htmlTemplate)
  {
    return Content(htmlTemplate);
  }
}
