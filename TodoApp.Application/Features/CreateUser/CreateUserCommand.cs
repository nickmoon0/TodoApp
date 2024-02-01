namespace TodoApp.Application.Features.CreateUser;

public class CreateUserCommand(string username, string password)
{
    public string Username { get; } = username;
    public string Password { get; } = password;
}