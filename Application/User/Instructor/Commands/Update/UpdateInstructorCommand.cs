using Application.Instructor.Dto;
using Domain.common;
using Domain.Model.Instructor;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Instructor.Commands.Update;

public class UpdateInstructorCommand:IRequest<Result<InstructorDto>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public Status Status { get; set; }
    public string? DepartmentId { get; set; }
    
    public class Validator:AbstractValidator<UpdateInstructorCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.Phone).NotEmpty();
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
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var instructor = applicationDbContext.Instructors.FirstOrDefault();
            yield return SwaggerExample.Create("example",instructor?.Adapt<UpdateInstructorCommand>() ?? new UpdateInstructorCommand());
        }
    }
}