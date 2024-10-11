namespace NShop.src.Application.Services;

using AutoMapper;
using Database;
using Core.Jwt;
using NShop.src.Application.Shared.Constant;

public abstract class BaseService
{
  protected readonly NShopDbContext _dbContext;
  protected readonly IMapper _mapper;
  protected readonly IHttpContextAccessor _httpCtx;

  public BaseService(NShopDbContext dbContext, IMapper mapper, IHttpContextAccessor httpCtx)
  {
    _dbContext = dbContext;
    _mapper = mapper;
    _httpCtx = httpCtx;
  }

  protected Payload? ExtractPayload()
  {
    var ctx = _httpCtx.HttpContext;
    if (ctx == null) return null;
    var payload = ctx.Items[JwtConst.PAYLOAD_KEY] as Payload;
    return payload;
  }
}