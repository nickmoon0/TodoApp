using MongoDB.Bson.Serialization.Attributes;

namespace Todo.Application.Models;

public class Item
{
    [BsonId]
    public Guid ItemId { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool Completed { get; set; }
}