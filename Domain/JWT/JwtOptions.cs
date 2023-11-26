namespace Domain.JWT;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateLifeTime { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }

}