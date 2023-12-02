using Application.Subject.Dto;
using Domain.common;
using Domain.Model.Subject;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Subject.Commands.Create;

public class Handler:IRequestHandler<CreateSubjectCommand,Result<SubjectDto>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly ApplicationDbContext _context;

    public Handler(ISubjectRepository subjectRepository, ApplicationDbContext context)
    {
        _subjectRepository = subjectRepository;
        _context = context;
    }

    public async Task<Result<SubjectDto>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        if (request.DepartmentId is not null)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if(department is null)
                return Result.Failure<SubjectDto>("Invalid Department Id");
        }
            
        var instructor = await _context.Instructors.FindAsync(request.InstructorId);
        if(instructor is null)
            return Result.Failure<SubjectDto>("Instructor Not Found");
            
        var subject = request.Adapt<Domain.Model.Subject.Subject>();
        var result = await _subjectRepository.Add(subject, cancellationToken);
        return result.IsSuccess
            ? result.Value.Adapt<SubjectDto>().AsSuccessResult()
            : Result.Failure<SubjectDto>(result.Error);
    }
}
