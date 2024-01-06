using Domain.common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Role;

public class Role:IdentityRole
{
    public string CreatedBy { get; set; }
}