using System.ComponentModel.DataAnnotations;
using Application.User.Instructor.Dto;
using Domain.common;
using Domain.Model.Instructor;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.User.Instructor.Commands.Create;

public class CreateInstructorCommand:IRequest<Result<InstructorDto>>
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
    public Status Status { get; set; }
    public string? DepartmentId { get; set; } 
    public class Validator:AbstractValidator<CreateInstructorCommand>
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
            var adminContext = scope.ServiceProvider.GetRequiredService<IAdminContext>();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            var username = adminContext.Tenants.Select(x => x.UserName).FirstOrDefault() ?? string.Empty;
            var departmentId = applicationDbContext.Departments.Select(x => x.Id).FirstOrDefault() ?? string.Empty;
            var command=new CreateInstructorCommand
            {
                FirstName = "Ahmed",
                LastName = "Mohamed",
                Address ="Cairo",
                PhoneNumber = "01274749316",
                UserName = "mahmoudazmy361",
                Email = "mahmoudazmy361@gmail.com",
                Password = "m@hmoud2019",
                ConfirmPassword = "m@hmoud2019",
                Status =Status.Employed
            };
            yield return SwaggerExample.Create("Required", command);
            command.DepartmentId = departmentId;
            yield return SwaggerExample.Create("Full", command);
        }
    }

}