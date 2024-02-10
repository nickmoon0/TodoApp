using Microsoft.AspNetCore.Http;
using TodoApp.Application.Common;
using TodoApp.Application.Common.Repositories;

namespace TodoApp.Application.Features.Auth.LoginUser;

public class LoginUserHandler : IHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly ITokenService _tokenService;
    
    public LoginUserHandler(IUserRepository userRepository, ITokenRepository tokenRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
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

        var tokenSet = await _tokenService.RotateTokens(user);
        
        // User credentials are a match
        return new LoginUserResponse
        {
            AccessToken = tokenSet.NewAccessToken,
            RefreshToken = tokenSet.NewRefreshToken.Token,
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }
}