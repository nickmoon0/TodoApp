using TodoApp.Application.Models;

namespace TodoApp.Application.Common.Repositories;

public interface IItemRepository
{
    public Task<Item?> GetItemByIdAsync(Guid id);
    public Task CreateItemAsync(Item item);
    public Task<List<Item>> GetUsersItemsAsync(Guid userId);
    public Task UpdateItemAsync(Item item);
    public Task DeleteItemAsync(Guid itemId);
}