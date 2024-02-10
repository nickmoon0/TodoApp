using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApp.Application.Common.Repositories;
using TodoApp.Application.Models.Auth;
using TodoApp.Infrastructure.Settings;

namespace TodoApp.Infrastructure.Data;

public class TokenRepository : ITokenRepository {
    private readonly IMongoCollection<RefreshToken> _tokenCollection;

    public TokenRepository(IOptions<MongoDbSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);

        _tokenCollection = database.GetCollection<RefreshToken>(settings.Value.TokenCollection);
    }

    public async Task CreateTokenAsync(RefreshToken token) => await _tokenCollection.InsertOneAsync(token);
    public async Task<RefreshToken?> GetTokenAsync(string token) => 
        await _tokenCollection.Find(x => x.Token == token).SingleOrDefaultAsync();

    public async Task<List<RefreshToken>> GetTokensByUserAsync(Guid userId) => 
        await _tokenCollection.Find(x => x.UserId == userId && x.Valid == true).ToListAsync();
    

    // Dont update token object when invalidating as no parameters aside from 'Valid' should ever be changed
    public async Task InvalidateTokenAsync(Guid tokenId)
    {
        var filter = Builders<RefreshToken>.Filter.Eq(x => x.Id, tokenId);
        var update = Builders<RefreshToken>.Update
            .Set(x => x.Valid, false);

        await _tokenCollection.UpdateOneAsync(filter, update);
    }
}