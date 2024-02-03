using Domain.common;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School;

namespace SchoolCleanArchitecture.Services.Installer;

public class DbContextInstall : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            var defaultConnection = configuration.GetConnectionString("DevelopmentConnection")!;
            var connection = TenantContainer.ConnectionString ?? defaultConnection;
            options.UseSqlServer(connection,
                x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });
        services.AddDbContext<IAdminContext, AdminContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!,
                x => x.MigrationsAssembly(typeof(AdminContext).Assembly.FullName)));
    }
}