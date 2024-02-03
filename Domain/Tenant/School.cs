using Domain.common;

namespace Domain.Tenant;

public class School:BaseEntity
{
    public string ArabicName { get; set; }
    public string EnglishName { get; set; }
    public HashSet<Tenant>? Tenants { get; set; }
    public HashSet<Role.Role>? Roles { get; set; }
}