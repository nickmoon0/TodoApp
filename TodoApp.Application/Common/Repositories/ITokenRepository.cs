using TodoApp.Application.Models.Auth;

namespace TodoApp.Application.Common.Repositories;

public interface ITokenRepository
{
    public Task CreateTokenAsync(RefreshToken token);
    public Task<RefreshToken?> GetTokenAsync(string token);
}