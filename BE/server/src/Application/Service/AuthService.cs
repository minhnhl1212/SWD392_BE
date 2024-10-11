namespace NShop.src.Application.Services;

using Database;
using AutoMapper;
using Core.Jwt;
using NShop.src.Application.Shared.Enum;
using NShop.src.Application.Shared.Constant;
using NShop.src.Application.DTOs.Auth;
using NShop.src.Domain.Users;
using System;
using Repository.Users;
using NShop.src.Application.DTOs.Users;

public interface IAuthService
{
  public Task<GgAuthResp> HandleGoogleLogin(string rdc, GgAuthInfo info);
}

public class AuthService : IAuthService
{
  // private readonly ICourseRepository _repo;
  private readonly IMapper _mapper;
  private readonly IJwtService _jwtService;
  private readonly IUserRepository _userRepo;

  public AuthService(IMapper mapper, NShopDbContext dbContext, IJwtService jwtService)
  {
    // _repo = new CourseRepository(dbContext);
    _mapper = mapper;
    _jwtService = jwtService;
    _userRepo = new UserRepository(dbContext);
  }

  private string GenerateAccessTk(Guid userId, Guid sessionId, string email, UserStatusEnum status)
  {
    return _jwtService.GenerateToken(userId, sessionId, email, status, JwtConst.ACCESS_TOKEN_EXP);
  }

  public async Task<GgAuthResp> HandleGoogleLogin(string rdc, GgAuthInfo info)
  {
    var user = await _userRepo.GetUserByEmail(info.Email);

    if (user == null)
    {
      var req = new UserCreateDto
      {
        FullName = info.FullName ?? "Guest",
        Email = info.Email,
        Status = UserStatusEnum.Active.ToString(),
      };
      var reqMapped = _mapper.Map<User>(req);

      reqMapped.RoleId = Guid.Parse(UserConst.DEFAULT_ROLE_ADMIN_ID);

      user = await _userRepo.AddAsync(reqMapped);
      if (user == null)
      {
        return new GgAuthResp
        {
          Success = false,
          Message = "Cannot create user",
        };
      }
    }

    var sessionId = Guid.NewGuid();
    var accessTk = GenerateAccessTk(user.Id, sessionId, user.Email, UserStatusEnum.Active);

    var redirectLink = rdc + "?access_token=" + accessTk + "&access_token_exp=" + DateTimeOffset.UtcNow.AddSeconds(JwtConst.ACCESS_TOKEN_EXP).ToUnixTimeSeconds();

    return new GgAuthResp
    {
      Success = true,
      RedirectLink = redirectLink,
    };
  }
}