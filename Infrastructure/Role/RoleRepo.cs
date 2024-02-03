using Domain.Role;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
namespace Infrastructure.Role;

public class RoleRepo:IRoleRepo
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly RoleManager<Domain.Role.Role> _roleManager;
    public RoleRepo(RoleManager<Domain.Role.Role> roleManager, IHttpContextAccessor httpContextAccessor)
    {
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<RoleDto> GenerateRole(string roleName)
    {
        // Get the currently logged-in user's name from HttpContext
        var userName = _httpContextAccessor.HttpContext?.User.FindFirst(nameof(Domain.Tenant.Tenant.UserName))?.Value;
        var identityRole = new Domain.Role.Role()
        {
            CreatedBy=userName??"username is null",
            Name = roleName
        };
        var result = await _roleManager.CreateAsync(identityRole);
        return result.Succeeded
            ? identityRole.Adapt<RoleDto>()
            : result.Errors.Adapt<RoleDto>();
    }
    

    public async Task<RoleDto> UpdateRole(string roleId,string roleName)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
            return ("Role Doesn't Exist").Adapt<RoleDto>();
        role.Name = roleName;
        var result = await _roleManager.UpdateAsync(role);
        return result.Succeeded
            ? role.Adapt<RoleDto>()
            : result.Errors.Adapt<RoleDto>();
    }

    public async Task<RoleDto> DeleteRole(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
            return ("Role Doesn't Exist").Adapt<RoleDto>();
        var result = await _roleManager.DeleteAsync(role);
        return result.Succeeded
            ? role.Adapt<RoleDto>()
            : result.Errors.Adapt<RoleDto>();
    }

    public async Task<bool> IsExistRule(string roleName)
    {
        /*var role =await _roleManager.FindByNameAsync(roleName);
        return role is not null;*/
        return await _roleManager.RoleExistsAsync(roleName);
    }
}