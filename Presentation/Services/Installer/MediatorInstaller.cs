using System.Reflection;
using Application.Department.Commands.Create;
using SchoolCleanArchitecture.middleware;
using FluentValidation;
using MediatR;
namespace SchoolCleanArchitecture.Services.Installer;

public class MediatorInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateDepartmentCommand).GetTypeInfo().Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineMiddleware<,>));
        services.AddValidatorsFromAssembly(typeof(CreateDepartmentCommand.Validator).Assembly);
    }
}