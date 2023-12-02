using Application.Subject.Dto;
using Domain.common;
using Domain.Model.Subject;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Subject.Commands.Create;

public class CreateSubjectCommand:IRequest<Result<SubjectDto>>
{
    public string Name { get; set; }
    public int Hours { get; set; }
    public string InstructorId { get; set; }
    public string? DepartmentId { get; set; }
    public class Validator:AbstractValidator<CreateSubjectCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Hours).NotEmpty();
            RuleFor(c => c.InstructorId).NotEmpty();
        }
    }

    
    
    public class Example : IMultipleExamplesProvider<CreateSubjectCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<CreateSubjectCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var instructorId = applicationDbContext.Instructors.Select(x => x.Id).FirstOrDefault() ?? string.Empty;
            var departmentId = applicationDbContext.Departments.Select(x => x.Id).FirstOrDefault() ?? string.Empty;

            var command=new CreateSubjectCommand()
            {
                Name ="Algorithms",
                Hours = 3,
                InstructorId = instructorId
            };
            yield return SwaggerExample.Create("Required", command);
            command.DepartmentId = departmentId;
            yield return SwaggerExample.Create("Full", command);

        }
    }

}