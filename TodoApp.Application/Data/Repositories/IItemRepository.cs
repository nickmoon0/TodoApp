using TodoApp.Application.Models;

namespace TodoApp.Application.Data.Repositories;

public interface IItemRepository
{
    public Task<Item> GetItemByIdAsync(Guid id);
    public Task CreateItemAsync(Item item);
    public Task<List<Item>> GetUsersItems(Guid userId);
}