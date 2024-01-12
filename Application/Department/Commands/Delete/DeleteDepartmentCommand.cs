using Application.Department.Dto;
using Domain.common;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.Department.Commands.Delete;

public class DeleteDepartmentCommand:IRequest<Result<DepartmentDto>>
{
    public string Id { get; set; }
    public class Validator:AbstractValidator<DeleteDepartmentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull();
        }
    }
    
    public class Example : IOperationFilter
    {
        private readonly IServiceScopeFactory? _scopeFactory;

        public Example(IServiceScopeFactory? scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.RelativePath != "api/Department/{id}" ||
                context.ApiDescription.HttpMethod != "DELETE")
                return;
            
            if (_scopeFactory is null) return;
            
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var department = dbContext.Departments
                .OrderByDescending(x => x.CreateDate)
                .Select(x => x.Id).FirstOrDefault() ?? string.Empty;

            foreach (var parameter in operation.Parameters)
            {
                if (string.Equals(parameter.Name, nameof(Id), StringComparison.CurrentCultureIgnoreCase))
                    parameter.Example = new OpenApiString(department);
            }
        }
    }

}