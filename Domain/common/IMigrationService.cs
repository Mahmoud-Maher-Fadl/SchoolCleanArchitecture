namespace Domain.common;

public interface IMigrationService
{
    Task CreateAndMigrateDatabaseAsync(string companyId, CancellationToken cancellationToken = default);
    Task DeleteDatabase(string companyId, CancellationToken cancellationToken = default);
}