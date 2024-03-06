using TodoApp.Api.Contracts.Responses;
using TodoApp.Api.Services;
using TodoApp.Application.Features.Auth.RenewAccessToken;

namespace TodoApp.Api.Endpoints;

public static class AuthEndpoints
{
    public static WebApplication RegisterAuthEndpoints(this WebApplication app, string groupPrefix)
    {
        var group = app.MapGroup(groupPrefix)
            .WithTags("Auth Endpoints")
            .WithOpenApi();

        var service = app.Services.GetRequiredService<IAuthService>();

        group.MapGet("/logout", service.LogoutUser)
            .RequireAuthorization()
            .WithSummary("Logs out user")
            .Produces(StatusCodes.Status204NoContent);
        
        group.MapPost("/login", service.LoginUser)
            .WithSummary("Logs in a user and returns a token")
            .WithDescription("Returns a JWT if login successful, will return 401 otherwise")
            .Produces<LoginUserResponseContract>()
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/renew-token", service.RenewToken)
            .WithSummary("Extracts refresh token and, if valid, renews access token")
            .WithDescription(
                "Uses the User ID contained within the refresh token to determine which user to issue access token to")
            .Produces<RenewAccessTokenResponse>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
        
        return app;
    }
}