using Domain.Identity;

namespace Domain.JWT;

public interface IJwtRepo
{ 
    Task<string> GenerateToken(User user);
}