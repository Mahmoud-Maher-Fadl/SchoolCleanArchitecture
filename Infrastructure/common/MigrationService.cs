using Domain.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.common;

public class MigrationService:IMigrationService
{
    private readonly DbContextOptionsBuilder<ApplicationDbContext> _optionsBuilder;
    private readonly string _defaultConnectionString;

    public MigrationService(IConfiguration configuration)
    {
        _optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        _defaultConnectionString = configuration.GetConnectionString("DevelopmentConnection")!;
    }

    public async Task CreateAndMigrateDatabaseAsync(string companyId, CancellationToken cancellationToken = default)
    {
        var newConnectionString = _defaultConnectionString.GetNewConnectionString(companyId);
        _optionsBuilder.UseSqlServer(newConnectionString);
        await using var context = new ApplicationDbContext(_optionsBuilder.Options);
        await context.Database.MigrateAsync(cancellationToken);
        await MigrateDatabaseAndAddSeeds(context);
    }

    public Task DeleteDatabase(string companyId, CancellationToken cancellationToken = default)
    {
        var newConnectionString = _defaultConnectionString.GetNewConnectionString(companyId);
        _optionsBuilder.UseSqlServer(newConnectionString);
        using var context = new ApplicationDbContext(_optionsBuilder.Options);
        return context.Database.EnsureDeletedAsync(cancellationToken);
    }

    private static async Task MigrateDatabaseAndAddSeeds(IApplicationDbContext context)
    {
        var seeds = typeof(ISeedGenerator).Assembly.DefinedTypes
            .Where(x => typeof(ISeedGenerator).IsAssignableFrom(x) &&
                        x is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance).Cast<ISeedGenerator>().ToList();
        seeds.ForEach(x => x.Generate(context));
        await context.SaveChangesAsync();
    }
}