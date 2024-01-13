using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Waffles_Club.Data.Entity;
using Waffles_Club.Service.Services.Interfaces;

namespace Waffles_Club.Service.Services.Implementations;

public class TokenService:ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<TokenService> _logger;

    public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public List<Claim> CreateClaims(User user, List<Role> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Email, user.Name),
            new Claim(ClaimTypes.Role, string.Join(" ", roles.Select(x => x.Name))),
        };
        _logger.LogInformation("Create claims");
        return claims;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:Expire"])),
            signingCredentials: credentials
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        _logger.LogInformation("Generated access token");
        return tokenHandler.WriteToken(jwtToken);
    }
}