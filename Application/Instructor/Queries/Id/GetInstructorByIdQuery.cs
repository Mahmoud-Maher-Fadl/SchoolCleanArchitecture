using Application.Instructor.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.Instructor.Queries.Id;

public class GetInstructorByIdQuery:IRequest<Result<InstructorDto>>
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
            if (context.ApiDescription.RelativePath != "api/Instructor/{id}" ||
                context.ApiDescription.HttpMethod != "GET")
                return;

            if (_scopeFactory is null) return;

            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var instructor = dbContext.Instructors
                .Select(x => x.Id)
                .FirstOrDefault() ?? string.Empty;

            operation.Parameters.First().Example = new OpenApiString(instructor);

        }
    }

}