using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApp.Application.Common.Repositories;
using TodoApp.Application.Models;
using TodoApp.Infrastructure.Settings;

namespace TodoApp.Infrastructure.Data;

public class ItemRepository : IItemRepository
{
    private readonly IMongoCollection<Item> _itemsCollection;
    
    public ItemRepository(IOptions<MongoDbSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);

        _itemsCollection = database.GetCollection<Item>(settings.Value.ItemsCollection);
    }

    public async Task<Item?> GetItemByIdAsync(Guid id) =>
        await _itemsCollection.Find(x => x.ItemId == id).SingleOrDefaultAsync();
    
    public async Task CreateItemAsync(Item item) => await _itemsCollection.InsertOneAsync(item);

    public async Task<List<Item>> GetUsersItems(Guid userId) =>
        await _itemsCollection.Find(x => x.UserId == userId).ToListAsync();

    public async Task UpdateItemAsync(Item item)
    {
        var filter = Builders<Item>.Filter.Eq(x => x.ItemId, item.ItemId);
        var update = Builders<Item>.Update
            .Set(x => x.Name, item.Name)
            .Set(x => x.Description, item.Description)
            .Set(x => x.Completed, item.Completed);

        await _itemsCollection.UpdateOneAsync(filter, update);
    }
}