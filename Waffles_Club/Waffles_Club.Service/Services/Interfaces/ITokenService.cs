using System.Security.Claims;
using Waffles_Club.Data.Entity;

namespace Waffles_Club.Service.Services.Interfaces;

public interface ITokenService
{
    List<Claim> CreateClaims(User user, List<Role> roles);
    string GenerateAccessToken(IEnumerable<Claim> claims);
}