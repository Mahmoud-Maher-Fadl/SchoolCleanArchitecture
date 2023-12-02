using Application.Instructor.Dto;
using Domain.common;
using Domain.Model.Instructor;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.Instructor.Commands.Delete;

public class DeleteInstructorCommand:IRequest<Result<InstructorDto>>
{
    public string Id { get; set; }
    public class Handler:IRequestHandler<DeleteInstructorCommand,Result<InstructorDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IInstructorRepository _instructorRepository;

        public Handler(ApplicationDbContext context, IInstructorRepository instructorRepository)
        {
            _context = context;
            _instructorRepository = instructorRepository;
        }

        public async Task<Result<InstructorDto>> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = await _context.Instructors.FindAsync(request.Id);
            if(instructor is null)
                return Result.Failure<InstructorDto>("Instructor Not Found");
            var result = await _instructorRepository.Delete(instructor, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<InstructorDto>().AsSuccessResult()
                : Result.Failure<InstructorDto>(result.Error);
        }
    }
    
    public class Example : IOperationFilter
    {
        private readonly IServiceScopeFactory? _scopeFactory;

        public Example(IServiceScopeFactory? scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.RelativePath != "api/Instructor/{id}" ||
                context.ApiDescription.HttpMethod != "DELETE")
                return;
            
            if (_scopeFactory is null) return;
            
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var instructor = dbContext.Instructors
                .OrderByDescending(x => x.CreateDate)
                .Select(x => x.Id).FirstOrDefault() ?? string.Empty;

            foreach (var parameter in operation.Parameters)
            {
                if (string.Equals(parameter.Name, nameof(Id), StringComparison.CurrentCultureIgnoreCase))
                    parameter.Example = new OpenApiString(instructor);
            }
        }
    }

}