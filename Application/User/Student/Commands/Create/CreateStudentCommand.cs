using System.ComponentModel.DataAnnotations;
using Application.User.Student.Dto;
using Domain.common;
using Domain.Model.Student;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.User.Student.Commands.Create;

public class CreateStudentCommand:IRequest<Result<StudentDto>>
{
    public string[]? Roles { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    public StudentStatus Status { get; set; }
    public string? DepartmentId { get; set; } = null;
    public List<string>? SubjectsId { get; set; } = null;
    public class Validator:AbstractValidator<CreateStudentCommand>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(c => c.Status).IsInEnum();
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
            var departmentId = applicationDbContext.Departments.Select(x => x.Id).FirstOrDefault() ?? null;
            var subjectId = applicationDbContext.Subjects.Select(x => x.Id).FirstOrDefault() ?? null;
            var command=new CreateStudentCommand()
            {
                FirstName = "Ahmed",
                LastName = "Mohamed",
                Address ="Cairo",
                PhoneNumber = "01274749316",
                UserName = "mahmoudazmy361",
                Email = "mahmoudazmy361@gmail.com",
                Password = "m@hmoud2019",
                ConfirmPassword = "m@hmoud2019",
                Status =StudentStatus.Student
            };
            yield return SwaggerExample.Create("Required", command);
            command.DepartmentId = departmentId;
            command.SubjectsId = new List<string>() { subjectId };
            yield return SwaggerExample.Create("Full", command);
        }
    }
}