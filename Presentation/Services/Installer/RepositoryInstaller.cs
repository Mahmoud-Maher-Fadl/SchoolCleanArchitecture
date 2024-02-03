using Domain.common;
using Domain.Model.Department;
using Infrastructure.common;

namespace SchoolCleanArchitecture.Services.Installer;

public class RepositoryInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var repositories = typeof(IBaseRepository<>).Assembly.ExportedTypes
            .Where(x => x.IsInterface && x.GetInterfaces().FirstOrDefault()?.Name.Contains(nameof(IBaseRepository<Department>)) == true)
            .ToList();

        foreach (var repository in repositories)
        {
            var entityType = repository.GetInterfaces().First().GetGenericArguments().First();
            var implementation = typeof(BaseSqlRepositoryImpl<>).Assembly.ExportedTypes.FirstOrDefault(x =>
                x.BaseType == typeof(BaseSqlRepositoryImpl<>).MakeGenericType(entityType));
            if (implementation == null)
                continue;
            services.AddScoped(repository,
                builder =>
                {
                    var context = builder.GetRequiredService<IApplicationDbContext>();
                    var instance = Activator.CreateInstance(implementation, context);
                    return instance;
                });
        }
    }
}