using TodoApp.Application.Models;
using TodoApp.Application.Models.Auth;

namespace TodoApp.Application.Common;

public interface ITokenService
{
    public string GenerateAccessToken(User user);
    public RefreshToken GenerateRefreshToken(User user);
    public Guid ExtractUserIdFromAccessToken(string token);
}