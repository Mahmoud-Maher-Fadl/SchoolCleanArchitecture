using Domain.Tenant;

namespace Domain.JWT;

public interface IJwtRepo
{ 
    Task<string> GenerateToken(Tenant.Tenant tenant);
}