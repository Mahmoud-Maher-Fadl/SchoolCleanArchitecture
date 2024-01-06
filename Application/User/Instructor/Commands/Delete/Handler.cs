using Application.Instructor.Commands.Delete;
using Application.Instructor.Dto;
using Domain.common;
using Domain.Model.Instructor;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.User.Instructor.Commands.Delete;
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
