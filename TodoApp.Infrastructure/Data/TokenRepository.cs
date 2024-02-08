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
    
}