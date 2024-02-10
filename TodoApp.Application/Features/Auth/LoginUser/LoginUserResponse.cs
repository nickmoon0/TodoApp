namespace TodoApp.Application.Features.Auth.LoginUser;

public class LoginUserResponse : IResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}