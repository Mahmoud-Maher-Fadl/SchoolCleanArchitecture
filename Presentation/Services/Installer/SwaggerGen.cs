using System.Security.Claims;
using Application.Department.Commands;
using Application.Department.Commands.Delete;
using Domain.JWT;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SchoolCleanArchitecture.Services.Installer;

public class SwaggerGen :IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Title", Version = "v1" });
            c.EnableAnnotations();

            
            c.EnableAnnotations();
            c.ExampleFilters();
            typeof(DeleteDepartmentCommand.Example).Assembly
                .ExportedTypes
                .Where(x => x.IsAssignableTo(typeof(IOperationFilter)))
                .ToList()
                .ForEach(x => c.OperationFilterDescriptors.Add(new FilterDescriptor
                    { Type = x, Arguments = Array.Empty<object>() }));
            
            // Add security definitions and requirements (if needed)
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}