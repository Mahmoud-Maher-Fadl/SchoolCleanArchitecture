using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.JWT;

public class JwtRepo:IJwtRepo
{
    private readonly JwtOptions _jwtOptions;
    private readonly RoleManager<Domain.Role.Role> _roleManager;
    private readonly UserManager<Domain.Identity.User> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    public JwtRepo(JwtOptions jwtOptions, IHttpContextAccessor contextAccessor, ApplicationDbContext context, RoleManager<Domain.Role.Role> roleManager, UserManager<Domain.Identity.User> userManager)
    {
        _jwtOptions = jwtOptions;
        _contextAccessor = contextAccessor;
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<string> GenerateToken(Domain.Identity.User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>()
        {
            new Claim(nameof(Domain.Identity.User.UserName),user.UserName),
            new Claim(nameof(Domain.Identity.User.Email),user.Email),
            new Claim(nameof(Domain.Identity.User.PhoneNumber),user.PhoneNumber),
        };
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role,role));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
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