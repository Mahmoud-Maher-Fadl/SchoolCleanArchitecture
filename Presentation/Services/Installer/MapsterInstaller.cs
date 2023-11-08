using System.Reflection;
using Mapster;
using MapsterMapper;

namespace SchoolCleanArchitecture.Services.Installer;

public class MapsterInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly(), typeof(Application.Department.Dto.DepartmentDto).Assembly);
        var mapper = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapper);
    }
}