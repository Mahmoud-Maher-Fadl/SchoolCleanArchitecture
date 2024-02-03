using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.common;
using Domain.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.JWT;

public class JwtRepo:IJwtRepo
{
    private readonly JwtOptions _jwtOptions;
    private readonly RoleManager<Domain.Role.Role> _roleManager;
    private readonly UserManager<Domain.Tenant.Tenant> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    public JwtRepo(JwtOptions jwtOptions, IHttpContextAccessor contextAccessor, IApplicationDbContext context, RoleManager<Domain.Role.Role> roleManager, UserManager<Domain.Tenant.Tenant> userManager)
    {
        _jwtOptions = jwtOptions;
        _contextAccessor = contextAccessor;
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<string> GenerateToken(Domain.Tenant.Tenant tenant)
    {
        var roles = await _userManager.GetRolesAsync(tenant);
        var claims = new List<Claim>()
        {
            new Claim(nameof(Domain.Tenant.Tenant.UserName),tenant.UserName),
            new Claim(nameof(Domain.Tenant.Tenant.Email),tenant.Email),
            new Claim(nameof(Domain.Tenant.Tenant.PhoneNumber),tenant.PhoneNumber),
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