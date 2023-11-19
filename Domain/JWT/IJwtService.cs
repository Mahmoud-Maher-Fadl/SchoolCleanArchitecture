using Domain.Identity;

namespace Domain.JWT;

public interface IJwtService
{ 
    Task<string> GenerateToken(User user);
}