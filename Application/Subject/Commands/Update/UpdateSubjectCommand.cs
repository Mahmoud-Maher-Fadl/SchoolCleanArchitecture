using Application.Subject.Dto;
using Domain.common;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Subject.Commands.Update;

public class UpdateSubjectCommand:IRequest<Result<SubjectDto>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Hours { get; set; }
    public string? InstructorId { get; set; }
    public string? DepartmentId { get; set; }
    public class Validator:AbstractValidator<UpdateSubjectCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Hours).NotEmpty();
        }
    }
    
    /*
    public class Example : IMultipleExamplesProvider<UpdateSubjectCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<UpdateSubjectCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            var subject = applicationDbContext.Subjects.FirstOrDefault();
            yield return SwaggerExample.Create("example",subject?.Adapt<UpdateSubjectCommand>() ?? new UpdateSubjectCommand());
        }
    }
    */

}