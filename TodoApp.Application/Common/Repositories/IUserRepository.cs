﻿using TodoApp.Application.Models;

namespace TodoApp.Application.Common.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserByName(string name);
    public Task<User> GetUserByIdAsync(Guid id);
    public Task CreateUserAsync(User user);
}