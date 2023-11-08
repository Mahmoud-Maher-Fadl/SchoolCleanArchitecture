using Domain.common;
using Infrastructure;
using Infrastructure.common;

namespace SchoolCleanArchitecture.Services.Installer;

public class RepositoryInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var repositories = typeof(IBaseRepository<>).Assembly.ExportedTypes
            .Where(x => x.IsInterface && x.Name.EndsWith("Repository"))
            .ToList();
        
        foreach (var repository in repositories)
        {
            var entityType = repository.GetInterfaces().First().GetGenericArguments().First();
            var implementation = typeof(BaseSqlRepositoryImpl<>).Assembly.ExportedTypes.First(x =>
                x.BaseType == typeof(BaseSqlRepositoryImpl<>).MakeGenericType(entityType));

            services.AddScoped(repository,
                builder =>
                {
                    var context = builder.GetService<ApplicationDbContext>();
                    var instance = Activator.CreateInstance(implementation, context);
                    return instance;
                });
        }
    }
}