using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Common;
using TodoApp.Api.Contracts.Requests;
using TodoApp.Api.Contracts.Responses;
using TodoApp.Application.Common;
using TodoApp.Application.Features;
using TodoApp.Application.Features.Auth.LoginUser;
using TodoApp.Application.Features.Auth.LogoutUser;
using TodoApp.Application.Features.Auth.RenewAccessToken;

namespace TodoApp.Api.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly ITokenService _tokenService;
    public AuthService(ILogger<AuthService> logger, ITokenService tokenService)
    {
        _logger = logger;
        _tokenService = tokenService;
    }

    public async Task<IResult> LogoutUser(HttpContext context, [FromServices] IHandler<LogoutUserCommand, LogoutUserResponse> handler)
    {
        var token = TokenHelpers.GetAccessToken(context)!;
        var userId = _tokenService.ExtractUserIdFromAccessToken(token);

        var command = new LogoutUserCommand
        {
            UserId = userId
        };

        await handler.Handle(command);
        
        TokenHelpers.InvalidateRefreshTokenCookie(context);
        TokenHelpers.InvalidateAccessTokenCookie(context);
        
        return TypedResults.NoContent();
    }
    
    public async Task<IResult> LoginUser(
        HttpContext context,
        [FromBody] LoginUserContract contract, 
        [FromServices] IHandler<LoginUserCommand, LoginUserResponse> handler)
    {
        _logger.LogInformation("Request to login {Username}", contract.Username);
        
        var command = new LoginUserCommand
        {
            Username = contract.Username,
            Password = contract.Password
        };
        
        var response = await handler.Handle(command);

        if (response.Success)
        {
            _logger.LogInformation("User {Username} has authenticated", contract.Username);
            
            TokenHelpers.AddRefreshTokenCookie(context, response.RefreshToken!);
            TokenHelpers.AddAccessTokenCookie(context, response.AccessToken!);
            
            return TypedResults.Ok(new LoginUserResponseContract
            {
                AccessToken = response.AccessToken,
                StatusCode = response.StatusCode,
                Success = response.Success
            });
        }

        _logger.LogInformation("Failed to login user {Username}", contract.Username);
        return TypedResults.StatusCode(StatusCodes.Status401Unauthorized);
    }

    public async Task<IResult> RenewToken(
        HttpContext context, 
        IHandler<RenewAccessTokenCommand, RenewAccessTokenResponse> handler)
    {
        _logger.LogInformation("Request received to renew access token");
        
        var refreshToken = context.Request.Cookies["RefreshToken"]; 
        if (string.IsNullOrEmpty(refreshToken))
        {
            _logger.LogInformation("Refresh token was not present in request");
            return TypedResults.StatusCode(StatusCodes.Status400BadRequest);
        }

        var command = new RenewAccessTokenCommand { RefreshToken = refreshToken };
        var response = await handler.Handle(command);

        if (response.Success)
        {
            _logger.LogInformation("Replaced access token with token {AccessToken}", response.AccessToken);
            
            TokenHelpers.AddRefreshTokenCookie(context, response.RefreshToken!);
            TokenHelpers.AddAccessTokenCookie(context, response.AccessToken!);
            
            return TypedResults.Ok(new RenewAccessTokenResponseContract
            {
                AccessToken = response.AccessToken,
                Success = response.Success,
                StatusCode = response.StatusCode
            });
        }
        
        _logger.LogInformation("Failed to issue a new access token for {RefreshToken}", refreshToken);
        return TypedResults.StatusCode(response.StatusCode);
    }
}