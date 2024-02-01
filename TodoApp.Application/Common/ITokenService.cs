using TodoApp.Application.Models;

namespace TodoApp.Application.Common;

public interface ITokenService
{
    public string GenerateToken(User user);
}