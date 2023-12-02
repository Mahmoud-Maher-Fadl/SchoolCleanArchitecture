using System.ComponentModel.DataAnnotations;
using Application.User.Dto;
using Domain.common;
using Domain.Model.Instructor;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.User.Commands.Create;

public class CreateUserCommand:IRequest<Result<UserDto>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    public class Validator:AbstractValidator<CreateUserCommand>
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
        }   
    }
    public class Example : IMultipleExamplesProvider<CreateUserCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<CreateUserCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var departmentId = applicationDbContext.Departments.Select(x => x.Id).FirstOrDefault() ?? string.Empty;
            var command=new CreateUserCommand()
            {
                FirstName = "Ahmed",
                LastName = "Mohamed",
                Address ="Cairo",
                PhoneNumber = "01274749316",
                UserName = "mahmoudfadl361",
                Email = "mahmoudfadl361@gmail.com",
                Password = "mahmoud2019",
                ConfirmPassword = "mahmoud2019",
            };
            yield return SwaggerExample.Create("example", command);
        }
    }
}