using Application.Instructor.Dto;
using Domain.common;
using Domain.Model.Instructor;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Instructor.Commands.Create;

public class CreateInstructorCommand:IRequest<Result<InstructorDto>>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public Status Status { get; set; }
    public string? DepartmentId { get; set; } 
    public class Validator:AbstractValidator<CreateInstructorCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.Phone).NotEmpty();
            RuleFor(c => c.Status).IsInEnum();
        }        
    }
    
    public class Example : IMultipleExamplesProvider<CreateInstructorCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<CreateInstructorCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var departmentId = applicationDbContext.Departments.Select(x => x.Id).FirstOrDefault() ?? string.Empty;
            var command=new CreateInstructorCommand
            {
                Name ="Ahmed",
                Address ="Cairo",
                Phone ="01274749316",
                Status =Status.Employed
            };
            yield return SwaggerExample.Create("Required", command);
            command.DepartmentId = departmentId;
            yield return SwaggerExample.Create("Full", command);
        }
    }

}