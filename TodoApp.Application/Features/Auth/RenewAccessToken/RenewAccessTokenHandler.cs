using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TodoApp.Application.Common;
using TodoApp.Application.Common.Repositories;

namespace TodoApp.Application.Features.Auth.RenewAccessToken;

public class RenewAccessTokenHandler : IHandler<RenewAccessTokenCommand, RenewAccessTokenResponse>
{
    private readonly ILogger<RenewAccessTokenHandler> _logger;
    private readonly ITokenService _tokenService;
    private readonly ITokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;

    public RenewAccessTokenHandler(
        ILogger<RenewAccessTokenHandler> logger, 
        ITokenService tokenService,
        ITokenRepository tokenRepository, 
        IUserRepository userRepository)
    {
        _logger = logger;
        _tokenService = tokenService;
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
    }

    public async Task<RenewAccessTokenResponse> Handle(RenewAccessTokenCommand command)
    {
        // Get token
        var token = await _tokenRepository.GetTokenAsync(command.RefreshToken);
        if (token == null || token.ExpiryDate < DateTime.Now)
        {
            _logger.LogInformation("Token does not exist or has expired");
            return new RenewAccessTokenResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }   
        
        // Get user
        var user = await _userRepository.GetUserByIdAsync(token.UserId);
        
        // Rotate tokens
        var tokenSet = await _tokenService.RotateTokens(user);
        _logger.LogInformation("Successfully generated new access and refresh token");

        return new RenewAccessTokenResponse
        {
            AccessToken = tokenSet.NewAccessToken,
            RefreshToken = tokenSet.NewRefreshToken.Token,
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }
}