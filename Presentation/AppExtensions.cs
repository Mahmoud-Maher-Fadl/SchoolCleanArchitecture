using Domain.common;
using Infrastructure;
using Infrastructure.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using School;

namespace SchoolCleanArchitecture;

public static class AppExtensions
{
    public static async Task MigrateAdminDb(this IHost webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<IAdminContext>();
        if (context is AdminContext dbContext)
            await dbContext.Database.MigrateAsync();
        
    }
    public static async Task MigrateTenantsDb(this IHost webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var services = scope.ServiceProvider;

        var adminContext = services.GetRequiredService<IAdminContext>();
        var companies = await adminContext.Schools.Select(x=>x.Id).ToListAsync();
        foreach (var id in companies)
        {
            var migrationService = services.GetRequiredService<IMigrationService>();
            await migrationService.CreateAndMigrateDatabaseAsync(id);
        }
    }
    public static void AddStaticFiles(this WebApplication webApplication)
    {
        if (!Directory.Exists(Path.Join(webApplication.Environment.ContentRootPath, "Images")))
            Directory.CreateDirectory(Path.Join(webApplication.Environment.ContentRootPath, "Images"));

        webApplication.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(webApplication.Environment.ContentRootPath, "Images")),
            RequestPath = "/Images"
        });
    }
}