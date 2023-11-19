using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.JWT;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.JWT;

public class JwtService:IJwtService
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public async Task<string> GenerateToken(Domain.Identity.User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(nameof(Domain.Identity.User.UserName),user.UserName),
            new Claim(nameof(Domain.Identity.User.Email),user.Email),
            new Claim(nameof(Domain.Identity.User.PhoneNumber),user.PhoneNumber),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken
        (
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(30),
            claims: claims, 
            signingCredentials: cred
        );
        return new JwtSecurityTokenHandler().WriteToken(token); 
    }
}