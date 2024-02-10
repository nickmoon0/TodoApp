namespace TodoApp.Application.Features.Auth.IssueAccessToken;

public class IssueAccessTokenCommand {
    public required string OldAccessToken { get; set; }
    public required string RefreshToken { get; set; }
}