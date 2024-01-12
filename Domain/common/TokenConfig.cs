namespace Domain.common;

public record TokenConfig(string Issuer, string Audience, long DurationInMinutes, string Secret);