namespace TodoApp.Api.Common;

public static class TokenHelpers
{
    public const string RefreshTokenName = "RefreshToken";
    public const string AccessTokenName = "AccessToken";

    public static string? GetAccessToken(HttpContext context)
    {
        try
        {
            var token = context.Request.Headers.Authorization[0]!.Split(' ')[1];
            return token;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static void AddRefreshTokenCookie(HttpContext context, string token)
    {
        // Create refresh token cookie (set to http-only)
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddDays(7) // TODO: Get lifespan from appsettings
        };
        context.Response.Cookies.Append(RefreshTokenName, token, cookieOptions);
    }

    public static void AddAccessTokenCookie(HttpContext context, string token)
    {
        var cookieOptions = new CookieOptions 
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddMinutes(10) // TODO: Get lifespan from appsettings
        };
        context.Response.Cookies.Append(AccessTokenName, token, cookieOptions);
    }

    public static void InvalidateAccessTokenCookie(HttpContext context)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddDays(-1)
        };
        context.Response.Cookies.Append(AccessTokenName, "", cookieOptions);
    }
    public static void InvalidateRefreshTokenCookie(HttpContext context)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddDays(-1)
        };
        context.Response.Cookies.Append(RefreshTokenName, "", cookieOptions);
    }
}