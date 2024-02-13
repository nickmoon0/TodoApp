using TodoApp.Api.Contracts;
using TodoApp.Api.Contracts.Requests;
using TodoApp.Application.Features;
using TodoApp.Application.Features.Auth.LoginUser;
using TodoApp.Application.Features.Auth.RenewAccessToken;

namespace TodoApp.Api.Services;

public interface IAuthService
{
    public Task<IResult> LoginUser(
        HttpContext context,
        LoginUserContract contract,
        IHandler<LoginUserCommand,LoginUserResponse> handler);

    public Task<IResult> RenewToken(
        HttpContext context, 
        IHandler<RenewAccessTokenCommand, RenewAccessTokenResponse> handler);
}