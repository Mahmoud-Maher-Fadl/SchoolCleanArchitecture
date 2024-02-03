using Domain.common;
using Domain.Model.Department;
using Infrastructure.common;
using School.Tenant;

namespace SchoolCleanArchitecture.Services.Installer;

public class AdminRepositoriesInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var repositories = typeof(IBaseAdminRepository<>).Assembly.ExportedTypes
            .Where(x => x.IsInterface &&
                        x.GetInterfaces().FirstOrDefault()?.Name.Contains(nameof(IBaseAdminRepository<Domain.Tenant.School>)) ==
                        true)
            .ToList();

        foreach (var repository in repositories)
        {
            var entityType = repository.GetInterfaces().First().GetGenericArguments().First();
            var implementation = typeof(TenantConfiguration).Assembly.ExportedTypes.FirstOrDefault(x =>
                x.BaseType == typeof(BaseSqlRepositoryImpl<>).MakeGenericType(entityType));
            if (implementation == null)
                continue;
            services.AddScoped(repository,
                builder =>
                {
                    var context = builder.GetRequiredService<IAdminContext>();
                    var instance = Activator.CreateInstance(implementation, context);
                    return instance;
                });
        }
    }
}