using Application.Department.Commands;
using Application.Student.Dto;
using Domain.common;
using Domain.Model.Student;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Student.Commands.Update;

public class UpdateStudentCommand:IRequest<Result<StudentDto>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string? DepartmentId { get; set; }

    public List<string>? SubjectsId { get; set; } = new List<string>();

    public class Validator:AbstractValidator<UpdateStudentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.Phone).NotEmpty();
        }
    }
    
    public class Example : IMultipleExamplesProvider<UpdateStudentCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<UpdateStudentCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var student = applicationDbContext.Students.FirstOrDefault();
            yield return SwaggerExample.Create("example",student?.Adapt<UpdateStudentCommand>() ?? new UpdateStudentCommand());
        }
    }

}