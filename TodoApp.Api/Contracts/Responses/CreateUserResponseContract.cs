namespace TodoApp.Api.Contracts.Responses;

public class CreateUserResponseContract
{
    public Guid UserId { get; set; }
    public string? Username { get; set; }
    public string? AccessToken { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}