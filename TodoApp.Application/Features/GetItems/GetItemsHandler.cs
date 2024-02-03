using Microsoft.AspNetCore.Http;
using TodoApp.Application.Common.Repositories;

namespace TodoApp.Application.Features.GetItems;

public class GetItemsHandler : IHandler<GetItemsCommand, GetItemsResponse>
{
    private readonly IItemRepository _itemRepository;

    public GetItemsHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<GetItemsResponse> Handle(GetItemsCommand command)
    {
        var items = await _itemRepository.GetUsersItemsAsync(command.UserId);

        var response = new GetItemsResponse
        {
            Items = items,
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };

        return response;
    }
}