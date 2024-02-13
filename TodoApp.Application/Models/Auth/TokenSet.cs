namespace TodoApp.Application.Models.Auth;

public class TokenSet
{
    public required RefreshToken NewRefreshToken { get; set; }
    public required string NewAccessToken { get; set; }
}