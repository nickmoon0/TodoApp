using Todo.Application.Features.CreateUser;
using static TodoApp.Api.Services.UserService;

namespace TodoApp.Api.Endpoints;

public static class UserEndpoints
{
    public static WebApplication RegisterUserEndpoints(this WebApplication app, string groupPrefix)
    {
        var group = app.MapGroup(groupPrefix)
            .WithTags("User Endpoints")
            .WithOpenApi();

        group.MapPost("/create", CreateUser)
            .WithSummary("Creates a new user")
            .WithDescription("Will create a new user in the database with a Guid and a" +
                             " hashed password. (Hashed with BCrypt)")
            .Produces<CreateUserResponse>()
            .Produces(StatusCodes.Status409Conflict);
            
        return app;
    }
}