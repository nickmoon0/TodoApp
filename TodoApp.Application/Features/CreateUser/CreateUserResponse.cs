namespace TodoApp.Application.Features.CreateUser;

public class CreateUserResponse : IResponse
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}