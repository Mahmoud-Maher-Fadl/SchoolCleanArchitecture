using Infrastructure;
using Infrastructure.common;
using Microsoft.EntityFrameworkCore;

namespace SchoolCleanArchitecture;

public static class AppExtensions
{
    public static async Task AddSeeds(this IHost webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        var seeds = typeof(ISeedGenerator).Assembly.DefinedTypes
            .Where(x => typeof(ISeedGenerator).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance).Cast<ISeedGenerator>().ToList();
        seeds.ForEach(x => x.Generate(context));
        await context.SaveChangesAsync();
    }
}