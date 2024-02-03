using Application.Localization;
using Application.Subject.Dto;
using Domain.common;
using Domain.Model.Subject;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Subject.Commands.Delete;

public class Handler:IRequestHandler<DeleteSubjectCommand,Result<SubjectDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;

    public Handler(IApplicationDbContext context, ISubjectRepository subjectRepository, IStringLocalizer<SharedResources> stringLocalizer)
    {
        _context = context;
        _subjectRepository = subjectRepository;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<Result<SubjectDto>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _context.Subjects.Include(s=>s.Students).FirstOrDefaultAsync(s=>s.Id==request.Id,cancellationToken);
        if(subject is null)
            return Result.Failure<SubjectDto>(_stringLocalizer[SharedResourcesKeys.NotFound]);
        var result = await _subjectRepository.Delete(subject, cancellationToken);
        return result.IsSuccess
            ? result.Value.Adapt<SubjectDto>().AsSuccessResult()
            : Result.Failure<SubjectDto>(result.Error);
    }
}
