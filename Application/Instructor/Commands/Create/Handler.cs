using Application.Instructor.Dto;
using Domain.common;
using Domain.Model.Instructor;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Instructor.Commands.Create;

public class Handler:IRequestHandler<CreateInstructorCommand,Result<InstructorDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IInstructorRepository _instructorRepository;

    public Handler(IInstructorRepository instructorRepository, ApplicationDbContext context)
    {
        _instructorRepository = instructorRepository;
        _context = context;
    }

    public async Task<Result<InstructorDto>> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
    {
        if (request.DepartmentId is not null)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if (department is null)
                return Result.Failure<InstructorDto>("Department Not Found"); 
        }
        var instructor = request.Adapt<Domain.Model.Instructor.Instructor>();
        var result = await _instructorRepository.Add(instructor, cancellationToken);
        return result.IsSuccess
            ? result.Value.Adapt<InstructorDto>().AsSuccessResult()
            : Result.Failure<InstructorDto>(result.Error);
    }
}
