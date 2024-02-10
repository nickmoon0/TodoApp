using TodoApp.Application.Models.Auth;

namespace TodoApp.Api.Common;

public static class Helpers
{
    public static void AddRefreshTokenCookie(HttpContext context, string token)
    {
        // Create refresh token cookie (set to http-only)
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        };
        context.Response.Cookies.Append("RefreshToken", token, cookieOptions);
    }
}