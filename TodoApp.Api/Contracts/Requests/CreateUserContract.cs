namespace TodoApp.Api.Contracts.Requests;

public class CreateUserContract
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}