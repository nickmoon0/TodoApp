using Microsoft.AspNetCore.Mvc;
using Todo.Application.Features;
using Todo.Application.Features.CreateUser;
using TodoApp.Api.Contracts;

namespace TodoApp.Api.Services;

public static class UserService
{
    public static async Task<IResult> CreateUser(
        [FromBody] CreateUserContract contract, 
        [FromServices] IHandler<CreateUserCommand,CreateUserResponse> handler)
    {
        var command = new CreateUserCommand(contract.Username, contract.Password);
        var result = await handler.Handle(command);
        return result.Success ? TypedResults.Ok(result) : TypedResults.StatusCode(result.StatusCode);
    }
}