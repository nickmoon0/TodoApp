using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Todo.Application.Data.Repositories;
using Todo.Application.Models;

namespace Todo.Application.Features.CreateUser;

public class CreateUserHandler : IHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserHandler> _logger;
    public CreateUserHandler(IUserRepository userRepository, ILogger<CreateUserHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    public async Task<CreateUserResponse> Handle(CreateUserCommand command)
    {
        var existingUser = await _userRepository.GetUserByName(command.Username);
        if (existingUser != null)
        {
            _logger.LogInformation("User: \"{User}\" already exists.", command.Username);
            return new CreateUserResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status409Conflict
            };
        }
        _logger.LogInformation("User: \"{User}\" does not exist. Creating new user", command.Username);
        
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = command.Username,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password)
        };

        await _userRepository.CreateUserAsync(user);
        _logger.LogInformation("Successfully created user \"{User}\"", command.Username);
        
        return new CreateUserResponse
        {
            UserId = user.UserId,
            Username = user.Username,
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }
}