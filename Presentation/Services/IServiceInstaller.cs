namespace SchoolCleanArchitecture.Services;

public interface IServiceInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}

public static class ServiceInstallerExtensions
{
    public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
    {
        var installers = typeof(Program).Assembly.ExportedTypes
            .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>()
            .ToList();

        installers.ForEach(installer => installer.InstallServices(services, configuration));
    }
}