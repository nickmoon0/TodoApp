using Microsoft.AspNetCore.Http;
using TodoApp.Application.Common.Repositories;

namespace TodoApp.Application.Features.Auth.LogoutUser;

public class LogoutUserHandler : IHandler<LogoutUserCommand, LogoutUserResponse>
{
    private readonly ITokenRepository _tokenRepository;

    public LogoutUserHandler(ITokenRepository tokenRepository) { _tokenRepository = tokenRepository; }

    public async Task<LogoutUserResponse> Handle(LogoutUserCommand command)
    {
        var userTokens = await _tokenRepository.GetTokensByUserAsync(command.UserId);
        var invalidateTokensTask = userTokens
            .Select(token => _tokenRepository.InvalidateTokenAsync(token.Id))
            .ToList();

        await Task.WhenAll(invalidateTokensTask);

        var response = new LogoutUserResponse()
        {
            Success = true,
            StatusCode = StatusCodes.Status204NoContent
        };

        return response;
    }
}