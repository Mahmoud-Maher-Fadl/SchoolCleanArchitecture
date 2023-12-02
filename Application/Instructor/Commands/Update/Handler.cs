using Application.Instructor.Dto;
using Domain.common;
using Domain.Model.Instructor;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Instructor.Commands.Update;

public class Handler : IRequestHandler<UpdateInstructorCommand, Result<InstructorDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IInstructorRepository _instructorRepository;

    public Handler(ApplicationDbContext context, IInstructorRepository instructorRepository)
    {
        _context = context;
        _instructorRepository = instructorRepository;
    }
    public async Task<Result<InstructorDto>> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
    {
        var instructor = await _context.Instructors.FindAsync(request.Id);
        if (instructor is null)
            return Result.Failure<InstructorDto>("Instructor Not Found");
        if (request.DepartmentId is not null)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if (department is null)
                return Result.Failure<InstructorDto>("Department Not Found");
        }
        request.Adapt(instructor);
        var result = await _instructorRepository.Update(instructor, cancellationToken);
        return result.IsSuccess
            ? result.Value.Adapt<InstructorDto>().AsSuccessResult()
            : Result.Failure<InstructorDto>(result.Error);
    }
}
