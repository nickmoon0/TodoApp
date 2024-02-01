namespace TodoApp.Application.Features.LoginUser;

public class LoginUserResponse : IResponse
{
    public string? Token { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}