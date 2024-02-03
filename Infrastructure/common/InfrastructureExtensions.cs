using Microsoft.Extensions.Configuration;

namespace Infrastructure.common;

public static class InfrastructureExtensions
{
    public static string GetNewConnectionString(this IConfiguration configuration, string companyId)
    {
        var defaultConnectionString = configuration.GetConnectionString("DevelopmentConnection")!;
        var oldDatabaseName = defaultConnectionString.GetDatabaseName();
        return defaultConnectionString.Replace(oldDatabaseName, $"School_{companyId}");
    }

    public static string GetNewConnectionString(this string connectionString, string companyId)
    {
        var oldDatabaseName = connectionString.GetDatabaseName();
        return connectionString.Replace(oldDatabaseName, $"School_{companyId}");
    }

    private static string GetDatabaseSegment(this string connectionString)
    {
        return connectionString.Split(';')[1];
    }

    private static string GetDatabaseName(this string connectionString)
    {
        return connectionString.GetDatabaseSegment().Split('=')[1];
    }
}