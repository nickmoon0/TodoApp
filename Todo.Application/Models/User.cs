using MongoDB.Bson.Serialization.Attributes;

namespace Todo.Application.Models;

public class User
{
    [BsonId]
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;
    public string? HashedPassword { get; set; }
}