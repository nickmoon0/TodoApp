using MongoDB.Bson.Serialization.Attributes;

namespace TodoApp.Application.Models.Auth;

public class RefreshToken {
    [BsonId]
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpiryDate { get; set; }
}