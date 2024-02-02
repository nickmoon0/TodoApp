using TodoApp.Api.Services;
using TodoApp.Application.Features.CreateItem;

namespace TodoApp.Api.Endpoints;

public static class ItemEndpoints
{
    public static WebApplication RegisterItemEndpoints(this WebApplication app, string groupPrefix)
    {
        var group = app.MapGroup(groupPrefix)
            .RequireAuthorization()
            .WithOpenApi();

        var service = app.Services.GetRequiredService<IItemService>();

        group.MapPost("/create", service.CreateItem)
            .WithSummary("Creates an item")
            .WithDescription("Requires auth. Will return 401 if no auth token present")
            .Produces<CreateItemResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized);
        
        return app;
    }
}