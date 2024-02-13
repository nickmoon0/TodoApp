namespace TodoApp.Application.Features.Auth.LoginUser;

public class LoginUserCommand
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}