using Domain.common;
using Domain.Tenant;
using Microsoft.AspNetCore.Identity;

namespace Domain.Role;

public class Role:IdentityRole
{
    public string CreatedBy { get; set; }
    public string SchoolId { get; set; }
    public School School { get; set; }
}