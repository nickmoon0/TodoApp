using TodoApp.Application.Models;

namespace TodoApp.Application.Common;

public interface ITokenService
{
    public string GenerateAccessToken(User user);
    public Guid ExtractUserIdFromAccessToken(string token);
}