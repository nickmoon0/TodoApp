using Microsoft.AspNetCore.Http;
using TodoApp.Application.Common;
using TodoApp.Application.Common.Repositories;

namespace TodoApp.Application.Features.Auth.LoginUser;

public class LoginUserHandler : IHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    
    public LoginUserHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }
    public async Task<LoginUserResponse> Handle(LoginUserCommand command)
    {
        var user = await _userRepository.GetUserByName(command.Username);
        
        // No user exists with that name
        if (user == null)
        {
            return new LoginUserResponse()
            {
                Success = false,
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        // Check passwords are equal
        var passwordsMatch = BCrypt.Net.BCrypt.Verify(command.Password, user.HashedPassword);
        if (!passwordsMatch)
        {
            return new LoginUserResponse()
            {
                Success = false,
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user, accessToken);
        
        // User credentials are a match
        return new LoginUserResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }
}