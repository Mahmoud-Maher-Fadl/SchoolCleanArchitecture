using Application.Subject.Dto;
using Domain.common;
using Domain.Model.Subject;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subject.Commands.Update;

public class Handler:IRequestHandler<UpdateSubjectCommand,Result<SubjectDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ISubjectRepository _subjectRepository;

    public Handler(IApplicationDbContext context, ISubjectRepository subjectRepository)
    {
        _context = context;
        _subjectRepository = subjectRepository;
    }

    public async Task<Result<SubjectDto>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (subject is null)
            return Result.Failure<SubjectDto>("Subject Not Found");
        if (request.DepartmentId is not null)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if (department is null)
                return Result.Failure<SubjectDto>("Invalid Department ID");
        }
        var instructor = await _context.Instructors.FindAsync(request.InstructorId);
        if(instructor is null)
            return Result.Failure<SubjectDto>("Instructor Not Found");
            
        request.Adapt(subject);
        var result = await _subjectRepository.Update(subject, cancellationToken);
        return result.IsSuccess
            ? result.Value.Adapt<SubjectDto>().AsSuccessResult()
            : Result.Failure<SubjectDto>(result.Error);
    }
}
