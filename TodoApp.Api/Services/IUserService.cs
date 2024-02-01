using Microsoft.AspNetCore.Mvc;
using Todo.Application.Features;
using Todo.Application.Features.CreateUser;
using TodoApp.Api.Contracts;

namespace TodoApp.Api.Services;

public interface IUserService
{
    public Task<IResult> CreateUser(CreateUserContract contract,
        IHandler<CreateUserCommand, CreateUserResponse> handler);
}