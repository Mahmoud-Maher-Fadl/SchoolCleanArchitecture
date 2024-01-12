using Application.Subject.Dto;
using Domain.common;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.Subject.Queries.Id;

public class GetSubjectByIdQuery:IRequest<Result<SubjectDto>>
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
            if (context.ApiDescription.RelativePath != "api/Subject/{id}" ||
                context.ApiDescription.HttpMethod != "GET")
                return;

            if (_scopeFactory is null) return;

            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var subject = dbContext.Subjects
                .Select(x => x.Id)
                .FirstOrDefault() ?? string.Empty;

            operation.Parameters.First().Example = new OpenApiString(subject);

        }
    }
}