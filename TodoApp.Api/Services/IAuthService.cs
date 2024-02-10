using TodoApp.Api.Contracts;
using TodoApp.Application.Features;
using TodoApp.Application.Features.Auth.LoginUser;

namespace TodoApp.Api.Services;

public interface IAuthService
{
    public Task<IResult> LoginUser(LoginUserContract contract, IHandler<LoginUserCommand, LoginUserResponse> handler);
}