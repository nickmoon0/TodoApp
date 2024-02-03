using Microsoft.AspNetCore.Http;
using TodoApp.Application.Common.Repositories;
using TodoApp.Application.Models;

namespace TodoApp.Application.Features.UpdateItem;

public class UpdateItemHandler : IHandler<UpdateItemCommand, UpdateItemResponse>
{
    private readonly IItemRepository _itemRepository;

    public UpdateItemHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<UpdateItemResponse> Handle(UpdateItemCommand command)
    {
        var existingItem = await _itemRepository.GetItemByIdAsync(command.ItemId);
        
        // If item exists but does not belong to user, return not found
        var notFound = existingItem == null || existingItem.UserId != command.UserId;
        if (notFound)
        {
            return new UpdateItemResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        var item = new Item
        {
            ItemId = command.ItemId,
            Name = command.Name,
            Description = command.Description,
            Completed = command.Completed
        };

        await _itemRepository.UpdateItemAsync(item);
        return new UpdateItemResponse
        {
            UpdatedItem = item,
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }
}