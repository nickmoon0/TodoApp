using TodoApp.Application.Models;

namespace TodoApp.Application.Common;

public interface ITokenService
{
    public string GenerateAccessToken(User user);
    public string GenerateRefreshToken(User user, string accessToken);
    public Guid ExtractUserIdFromAccessToken(string token);
}