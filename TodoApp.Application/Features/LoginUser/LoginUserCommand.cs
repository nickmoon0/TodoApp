namespace TodoApp.Application.Features.LoginUser;

public class LoginUserCommand
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}