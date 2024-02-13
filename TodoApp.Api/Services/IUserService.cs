﻿using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features;
using TodoApp.Application.Features.CreateUser;
using TodoApp.Api.Contracts;
using TodoApp.Api.Contracts.Requests;

namespace TodoApp.Api.Services;

public interface IUserService
{
    public Task<IResult> CreateUser(CreateUserContract contract,
        IHandler<CreateUserCommand, CreateUserResponse> handler);
}