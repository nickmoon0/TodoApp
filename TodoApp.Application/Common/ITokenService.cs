using TodoApp.Application.Models;
using TodoApp.Application.Models.Auth;

namespace TodoApp.Application.Common;

public interface ITokenService
{
    public Task<TokenSet> RotateTokens(User user);
    public Guid ExtractUserIdFromAccessToken(string token);
}