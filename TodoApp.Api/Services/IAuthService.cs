using TodoApp.Api.Contracts;
using TodoApp.Api.Contracts.Requests;
using TodoApp.Application.Features;
using TodoApp.Application.Features.Auth.LoginUser;

namespace TodoApp.Api.Services;

public interface IAuthService
{
    public Task<IResult> LoginUser(
        HttpContext context,
        LoginUserContract contract,
        IHandler<LoginUserCommand,LoginUserResponse> handler);
}