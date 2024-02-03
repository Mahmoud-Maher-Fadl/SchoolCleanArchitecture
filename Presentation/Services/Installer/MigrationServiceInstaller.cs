using Domain.common;
using Infrastructure.common;

namespace SchoolCleanArchitecture.Services.Installer;

public class MigrationServiceInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMigrationService, MigrationService>();
    }
}