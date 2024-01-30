namespace Todo.Application.Features.CreateUser;

public class CreateUserCommand(string username, string password)
{
    public string Username { get; init; } = username;
    public string Password { get; init; } = password;
}