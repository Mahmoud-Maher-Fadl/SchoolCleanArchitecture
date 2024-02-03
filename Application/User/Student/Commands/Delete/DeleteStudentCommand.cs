using Application.User.Student.Dto;
using Domain.common;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.User.Student.Commands.Delete;

public class DeleteStudentCommand:IRequest<Result<StudentDto>>
{
    public string Id { get; set; }
    
    public class Example : IOperationFilter
    {
        private readonly IServiceScopeFactory? _scopeFactory;

        public Example(IServiceScopeFactory? scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.RelativePath != "api/Student/{id}" ||
                context.ApiDescription.HttpMethod != "DELETE")
                return;
            
            if (_scopeFactory is null) return;
            
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            var student = dbContext.Students
                .OrderByDescending(x => x.CreateDate)
                .Select(x => x.Id).FirstOrDefault() ?? string.Empty;

            foreach (var parameter in operation.Parameters)
            {
                if (string.Equals(parameter.Name, nameof(Id), StringComparison.CurrentCultureIgnoreCase))
                    parameter.Example = new OpenApiString(student);
            }
        }
    }

}