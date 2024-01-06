namespace Domain.JWT;

public class JwtAuthenticationResultDto
{
    public string accessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
public record RefreshToken(
    string UserName,
    string RefreshedToken,
    DateTime ExpiresAt
        );