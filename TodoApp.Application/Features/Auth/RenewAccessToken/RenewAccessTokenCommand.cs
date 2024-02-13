namespace TodoApp.Application.Features.Auth.RenewAccessToken;

public class RenewAccessTokenCommand {
    public required string RefreshToken { get; set; }
}