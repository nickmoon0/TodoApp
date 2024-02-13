using TodoApp.Application.Features;

namespace TodoApp.Api.Contracts.Responses;

public class RenewAccessTokenResponseContract : IResponse
{
    public string? AccessToken { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}