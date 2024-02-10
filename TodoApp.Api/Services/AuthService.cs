using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Contracts.Requests;
using TodoApp.Api.Contracts.Responses;
using TodoApp.Application.Features;
using TodoApp.Application.Features.Auth.LoginUser;
using TodoApp.Application.Features.Auth.RenewAccessToken;

namespace TodoApp.Api.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    
    public AuthService(ILogger<AuthService> logger)
    {
        _logger = logger;
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

            // Create refresh token cookie (set to http-only)
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            };
            context.Response.Cookies.Append("RefreshToken", response.RefreshToken!, cookieOptions);
            
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
            return TypedResults.Ok(response);
        }
        
        _logger.LogInformation("Failed to issue a new access token for {RefreshToken}", refreshToken);
        return TypedResults.StatusCode(response.StatusCode);
    }
}