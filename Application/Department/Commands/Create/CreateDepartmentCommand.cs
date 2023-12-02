using Application.Department.Dto;
using Domain.common;
using Domain.Model.Department;
using FluentValidation;
using Swashbuckle.AspNetCore.Filters;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Department.Commands.Create;

public class CreateDepartmentCommand:IRequest<Result<DepartmentDto>>
{
    public string Name { get; set; } 
    public string Location { get; set; } 
    public class Validator:AbstractValidator<CreateDepartmentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Location).NotEmpty();
        }
    }
    public class Example : IMultipleExamplesProvider<CreateDepartmentCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<CreateDepartmentCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            yield return SwaggerExample.Create("Example", new CreateDepartmentCommand()
            {
                Name = "Cs",
                Location = "Room 1",
            });
            
        }
    }

}