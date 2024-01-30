using Microsoft.AspNetCore.Http;
using Todo.Application.Data.Repositories;
using Todo.Application.Models;

namespace Todo.Application.Features.CreateUser;

public class CreateUserHandler : IHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<CreateUserResponse> Handle(CreateUserCommand command)
    {
        var existingUser = await _userRepository.GetUserByName(command.Username);
        if (existingUser != null)
        {
            return new CreateUserResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status409Conflict
            };
        }
        
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = command.Username,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password)
        };

        await _userRepository.CreateUserAsync(user);

        return new CreateUserResponse
        {
            UserId = user.UserId,
            Username = user.Username,
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }
}