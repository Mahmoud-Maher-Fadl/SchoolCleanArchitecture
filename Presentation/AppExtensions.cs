using Infrastructure;
using Infrastructure.common;
using Microsoft.EntityFrameworkCore;

namespace Erp;

public static class AppExtensions
{
    public static async Task AddSeeds(this IHost webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        var seeds = typeof(SeedGenerator).Assembly.DefinedTypes
            .Where(x => typeof(SeedGenerator).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance).Cast<SeedGenerator>().ToList();
        seeds.ForEach(x => x.Generate(context));
        await context.SaveChangesAsync();
    }
}