using TodoApp.Api.Contracts.Responses;
using TodoApp.Api.Services;

namespace TodoApp.Api.Endpoints;

public static class AuthEndpoints
{
    public static WebApplication RegisterAuthEndpoints(this WebApplication app, string groupPrefix)
    {
        var group = app.MapGroup(groupPrefix)
            .WithTags("Auth Endpoints")
            .WithOpenApi();

        var service = app.Services.GetRequiredService<IAuthService>();
        
        group.MapPost("/login", service.LoginUser)
            .WithSummary("Logs in a user and returns a token")
            .WithDescription("Returns a JWT if login successful, will return 401 otherwise")
            .Produces<LoginUserResponseContract>()
            .Produces(StatusCodes.Status401Unauthorized);
            
        return app;
    }
}