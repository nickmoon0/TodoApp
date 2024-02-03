namespace TodoApp.Api.Contracts;

public class LoginUserContract
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}