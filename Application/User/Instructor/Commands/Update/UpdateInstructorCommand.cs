using System.ComponentModel.DataAnnotations;
using Application.User.Instructor.Dto;
using Domain.common;
using Domain.Model.Instructor;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.User.Instructor.Commands.Update;

public class UpdateInstructorCommand:IRequest<Result<InstructorDto>>
{
    public string Id { get; set; }
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

    
    public class Validator:AbstractValidator<UpdateInstructorCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
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

    public class Example : IMultipleExamplesProvider<UpdateInstructorCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<UpdateInstructorCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            var instructor = applicationDbContext.Instructors.FirstOrDefault();
            yield return SwaggerExample.Create("example",instructor?.Adapt<UpdateInstructorCommand>() ?? new UpdateInstructorCommand());
        }
    }
}