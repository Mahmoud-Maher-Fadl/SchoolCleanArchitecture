using Application.Department.Dto;
using Domain.common;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Department.Commands.Update;

public class UpdateDepartmentCommand:IRequest<Result<DepartmentDto>>
{
    public string Id { get; set; }
    public string Name { get; set; } 
    public string Location { get; set; } 
    
    public class Validator:AbstractValidator<UpdateDepartmentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Location).NotEmpty();
        }
        
    }
    
    public class Example : IMultipleExamplesProvider<UpdateDepartmentCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<UpdateDepartmentCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            var department = applicationDbContext.Departments.FirstOrDefault();
            yield return SwaggerExample.Create("example",department?.Adapt<UpdateDepartmentCommand>() ?? new UpdateDepartmentCommand());
        }
    }

}