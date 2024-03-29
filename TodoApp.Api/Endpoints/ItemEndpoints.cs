﻿using TodoApp.Api.Services;
using TodoApp.Application.Features.CreateItem;
using TodoApp.Application.Features.DeleteItem;
using TodoApp.Application.Features.GetItems;
using TodoApp.Application.Features.UpdateItem;

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

        group.MapPut("/update/{itemId:guid}", service.UpdateItem)
            .WithSummary("Updates an existing item")
            .WithDescription("Completely updates an object. Any field which is null in the request body will be set to null")
            .Produces<UpdateItemResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/delete/{itemId:guid}", service.DeleteItem)
            .WithSummary("Deletes an existing item")
            .WithDescription("Deletes a given item from the database. Will return 404 if the item does not belong to user")
            .Produces<DeleteItemResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/all", service.GetUsersItems)
            .WithSummary("Retrieves all of a users items")
            .WithDescription("Takes the user ID from the given JWT and gets all the users items")
            .Produces<GetItemsResponse>();
        
        return app;
    }
}