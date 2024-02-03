using Microsoft.AspNetCore.Http;
using TodoApp.Application.Common.Repositories;

namespace TodoApp.Application.Features.DeleteItem;

public class DeleteItemHandler : IHandler<DeleteItemCommand, DeleteItemResponse>
{
    private readonly IItemRepository _itemRepository;
    
    public DeleteItemHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }
    public async Task<DeleteItemResponse> Handle(DeleteItemCommand command)
    {
        var existingItem = await _itemRepository.GetItemByIdAsync(command.ItemId);
        
        // If item exists but does not belong to user, return 404
        var notFound = existingItem == null || existingItem.UserId != command.UserId;
        if (notFound)
        {
            return new DeleteItemResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        await _itemRepository.DeleteItemAsync(command.ItemId);
        return new DeleteItemResponse
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }
}