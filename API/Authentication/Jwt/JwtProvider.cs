using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using BooksAPI.Authentication.Jwt;
using BooksAPI.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BooksAPI.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        foreach (var role in user.UserRoles) claims.Add(new Claim(ClaimTypes.Role, role.Role.ToRoleString()));

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims, null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenHandler;
    }
}