using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Contracts;
using TodoApp.Application.Common;
using TodoApp.Application.Features;
using TodoApp.Application.Features.CreateItem;

namespace TodoApp.Api.Services;

public class ItemService : IItemService
{
    private readonly ILogger<ItemService> _logger;
    private readonly ITokenService _tokenService;
    
    public ItemService(ILogger<ItemService> logger, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<IResult> CreateItem(
        [FromBody] CreateItemContract contract,
        [FromServices] IHandler<CreateItemCommand, CreateItemResponse> handler,
        HttpContext context)
    {
        _logger.LogInformation("Request to create new item received");
        
        var token = context.Request.Headers.Authorization[0]!.Split(' ')[1];
        var userId = _tokenService.ExtractUserIdFromToken(token);

        _logger.LogInformation("Requested by user \"{UserId}\"", userId);
        
        var command = new CreateItemCommand
        {
            UserId = userId,
            Name = contract.Name,
            Description = contract.Description,
            Completed = contract.Completed
        };

        var response = await handler.Handle(command);

        if (!response.Success)
        {
            _logger.LogInformation("Failed to create item with code {Code}", response.StatusCode);
            return TypedResults.StatusCode(response.StatusCode);
        }

        var item = response.CreatedItem!;
        
        _logger.LogInformation("Created item with ID \"{ItemId}\"", item.ItemId);
        return TypedResults.Created($"/item/{item.ItemId}", item);
    }
}