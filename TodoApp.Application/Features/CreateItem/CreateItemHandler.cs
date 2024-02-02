using Microsoft.AspNetCore.Http;
using TodoApp.Application.Common;
using TodoApp.Application.Common.Repositories;
using TodoApp.Application.Models;

namespace TodoApp.Application.Features.CreateItem;

public class CreateItemHandler : IHandler<CreateItemCommand, CreateItemResponse>
{
    private readonly IItemRepository _itemRepository;
    
    
    public CreateItemHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<CreateItemResponse> Handle(CreateItemCommand command)
    {
        var newItem = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name,
            Description = command.Description,
            Completed = command.Completed
        };

        await _itemRepository.CreateItemAsync(newItem);

        var response = new CreateItemResponse
        {
            CreatedItem = newItem,
            Success = true,
            StatusCode = StatusCodes.Status201Created
        };
        
        return response;
    }
}