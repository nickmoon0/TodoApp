using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Application.Common;
using TodoApp.Application.Models;
using TodoApp.Infrastructure.Settings;

namespace TodoApp.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IOptions<JwtSettings> _settings;
    public TokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings;
    }
    
    public string GenerateToken(User user)
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
            expires: DateTime.Now.AddMinutes(_settings.Value.TokenLife),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}