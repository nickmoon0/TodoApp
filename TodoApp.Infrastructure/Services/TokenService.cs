using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Application.Common;
using TodoApp.Application.Common.Repositories;
using TodoApp.Application.Models;
using TodoApp.Application.Models.Auth;
using TodoApp.Infrastructure.Settings;

namespace TodoApp.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IOptions<JwtSettings> _settings;
    public TokenService(
        IOptions<JwtSettings> settings, 
        ITokenRepository tokenRepository)
    {
        _settings = settings;
        _tokenRepository = tokenRepository;
    }
    
    private string GenerateAccessToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

        Claim[] claims = [
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Username!)
        ];
        
        var token = new JwtSecurityToken(
            _settings.Value.Issuer,
            _settings.Value.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.Value.AccessTokenLife),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private RefreshToken GenerateRefreshToken(User user)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var refreshToken = new RefreshToken {
            UserId = user.UserId,
            Token = token,
            ExpiryDate = DateTime.UtcNow.AddDays(_settings.Value.RefreshTokenLife),
            Valid = true
        };

        return refreshToken;
    }

    public async Task<TokenSet> RotateTokens(User user)
    {
        // Get existing tokens and invalidate them
        var existingTokens = await _tokenRepository.GetTokensByUserAsync(user.UserId);
        foreach (var token in existingTokens)
        {
            // There should only be 1 or 0 valid tokens at a time, treat as a list in case there happen to be many
            await _tokenRepository.InvalidateTokenAsync(token.Id);
        }
        
        // Generate new access and refresh token
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken(user);

        // Add new refresh token to database
        await _tokenRepository.CreateTokenAsync(refreshToken);
        
        return new TokenSet
        {
            NewRefreshToken = refreshToken,
            NewAccessToken = accessToken
        };
    }

    public Guid ExtractUserIdFromAccessToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_settings.Value.Key);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = _settings.Value.Issuer,
            ValidAudience = _settings.Value.Audience,
            ClockSkew = TimeSpan.Zero // Immediate expiration
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);

        if (userIdClaim == null) throw new InvalidOperationException("User ID was not found in JWT");
        
        return Guid.Parse(userIdClaim.Value);
    }
}