using Application.Department.Commands.Create;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SchoolCleanArchitecture.Services.Installer;

public class SwaggerExamplesInstaller:IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            typeof(CreateDepartmentCommand.Example).Assembly
                .ExportedTypes
                .Where(x => x.IsAssignableTo(typeof(IOperationFilter)))
                .ToList()
                .ForEach(x => c.OperationFilterDescriptors.Add(new FilterDescriptor
                    { Type = x, Arguments = Array.Empty<object>() }));

        });
    }
}