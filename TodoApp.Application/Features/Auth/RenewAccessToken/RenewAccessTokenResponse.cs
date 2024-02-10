namespace TodoApp.Application.Features.Auth.RenewAccessToken;

public class RenewAccessTokenResponse : IResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}