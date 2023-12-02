using Application.Student.Dto;
using Domain.common;
using Domain.Model.Instructor;
using Domain.Model.Student;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Student.Commands.Create;

public class CreateStudentCommand:IRequest<Result<StudentDto>>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string? DepartmentId { get; set; }
    public List<string>? SubjectsId { get; set; } = new List<string>();
    public class Validator:AbstractValidator<CreateStudentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.Phone).NotEmpty();
        }
    }

    public class Example : IMultipleExamplesProvider<CreateStudentCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<CreateStudentCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var departmentId = applicationDbContext.Departments.Select(x => x.Id).FirstOrDefault() ?? string.Empty;
            var subjectId = applicationDbContext.Subjects.Select(x => x.Id).FirstOrDefault() ?? string.Empty;
            var command=new CreateStudentCommand()
            {
                Name ="Mahmoud",
                Address ="Cairo",
                Phone ="01274749316",
            };
            yield return SwaggerExample.Create("Required", command);
            command.DepartmentId = departmentId;
            command.SubjectsId = new List<string>() { subjectId };
            yield return SwaggerExample.Create("Full", command);
        }
    }
}