﻿using Application.Department.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.Department.Queries.Id;

public class GetDepartmentByIdQuery:IRequest<Result<DepartmentDto>>
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
            if (context.ApiDescription.RelativePath != "api/Department/{id}" ||
                context.ApiDescription.HttpMethod != "GET")
                return;

            if (_scopeFactory is null) return;

            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var department = dbContext.Departments
                .Select(x => x.Id)
                .FirstOrDefault() ?? string.Empty;

            operation.Parameters.First().Example = new OpenApiString(department);

        }
    }

}
