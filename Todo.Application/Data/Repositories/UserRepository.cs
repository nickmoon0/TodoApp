﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Todo.Application.Data.Settings;
using Todo.Application.Models;

namespace Todo.Application.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _usersCollection;
    
    public UserRepository(IOptions<MongoDbSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);

        _usersCollection = database.GetCollection<User>(settings.Value.UsersCollection);
    }

    public async Task<User?> GetUserByName(string name) =>
        await _usersCollection
            .Find(x => x.Username.Equals(name, StringComparison.CurrentCultureIgnoreCase))
            .SingleOrDefaultAsync();
    
    public async Task<User> GetUserByIdAsync(Guid id) => 
        await _usersCollection.Find(x => x.UserId == id).SingleAsync();

    public async Task CreateUserAsync(User user) => await _usersCollection.InsertOneAsync(user);
}