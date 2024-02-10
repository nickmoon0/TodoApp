using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features;
using TodoApp.Application.Features.CreateUser;
using TodoApp.Api.Contracts;
using TodoApp.Api.Contracts.Requests;

namespace TodoApp.Api.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;
    }
    public async Task<IResult> CreateUser(
        [FromBody] CreateUserContract contract, 
        [FromServices] IHandler<CreateUserCommand,CreateUserResponse> handler)
    {
        _logger.LogInformation("Received request to create user");
        
        var command = new CreateUserCommand(contract.Username, contract.Password);
        var result = await handler.Handle(command);
        
        _logger.LogInformation("Finished processing request to create user");

        if (result.Success)
        {
            _logger.LogInformation("Request was processed successfully");
            return TypedResults.Ok(result);
        }

        _logger.LogInformation("Request failed");
        return TypedResults.StatusCode(result.StatusCode);
    }
}