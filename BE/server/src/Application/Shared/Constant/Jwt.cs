namespace NShop.src.Application.Shared.Constant;

public static class JwtConst
{
  public const int ACCESS_TOKEN_EXP = 3600 * 24 * 30; // 15m
  public const int REFRESH_TOKEN_EXP = 3600 * 24 * 30; // 30 days

  public const string PAYLOAD_KEY = "payload";
}
