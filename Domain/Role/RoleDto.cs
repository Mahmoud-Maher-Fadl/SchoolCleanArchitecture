using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Domain.Role;

public class RoleDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CreatedBy { get; set; }
}