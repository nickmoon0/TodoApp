namespace TodoApp.Application.Features.Auth.LogoutUser;

public class LogoutUserResponse : IResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}