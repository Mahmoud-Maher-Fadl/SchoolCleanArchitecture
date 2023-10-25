using System.Reflection;
using Application.Department.Commands;
using Erp.middleware;
using FluentValidation;
using MediatR;

namespace Erp.Services.Installer;

public class MediatorInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateDepartmentCommand).GetTypeInfo().Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DbTransactionMiddleware<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineMiddleware<,>));
        services.AddValidatorsFromAssembly(typeof(CreateDepartmentCommand.Validator).Assembly);
    }
}