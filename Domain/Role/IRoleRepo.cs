using Domain.JWT;

namespace Domain.Role;

public interface IRoleRepo
{
    public Task<RoleDto> GenerateRole(string roleName);
    public Task<RoleDto> UpdateRole(string roleId,string roleName);
    public Task<RoleDto> DeleteRole(string roleName);
    public Task<bool> IsExistRule(string roleName);
}