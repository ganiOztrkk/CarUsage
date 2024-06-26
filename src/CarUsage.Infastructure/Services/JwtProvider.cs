using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarUsage.Core.ResultPattern;
using CarUsage.Domain.Entities;
using CarUsage.Domain.Options;
using CarUsage.Domain.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarUsage.Infastructure.Services;

public class JwtProvider(IOptions<Jwt> jwt) : IJwtProvider
{
    public Task<IDataResult<string>> CreateTokenAsync(AppUser user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName ?? string.Empty),
            new Claim("userId", user.Id.ToString()),
            new Claim("userFullName", string.Join(" ", user.Name, user.Lastname)),
            new Claim("username", user.UserName ?? string.Empty),
            new Claim("roles", string.Join(" ", roles))
        };

        var tokenExpires = DateTime.Now.AddMinutes(30);

        var securityToken = new JwtSecurityToken(
            issuer: jwt.Value.Issuer,
            audience: jwt.Value.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: tokenExpires,
            signingCredentials: 
            new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Value.SecretKey!)), SecurityAlgorithms.HmacSha512));
        
        JwtSecurityTokenHandler handler = new();
        var token = handler.WriteToken(securityToken);

        return Task.FromResult<IDataResult<string>>(new SuccessDataResult<string>(data: token));
    }
}