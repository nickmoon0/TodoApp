using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Contracts;
using TodoApp.Application.Features;
using TodoApp.Application.Features.Auth.LoginUser;

namespace TodoApp.Api.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;

    public AuthService(ILogger<AuthService> logger)
    {
        _logger = logger;
    }

    public async Task<IResult> LoginUser(
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
            return TypedResults.Ok(response);
        }

        _logger.LogInformation("Failed to login user {Username}", contract.Username);
        return TypedResults.StatusCode(StatusCodes.Status401Unauthorized);
    }
}