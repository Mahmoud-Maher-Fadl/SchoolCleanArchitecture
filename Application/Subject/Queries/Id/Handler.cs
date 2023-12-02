using Application.Subject.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subject.Queries.Id;
public class Handler:IRequestHandler<GetSubjectByIdQuery,Result<SubjectDto>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<SubjectDto>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
    {
        var subject = await _context.Subjects
            .Include(x => x.Department)
            .Include(x => x.Instructor)
            .Include(x=>x.Students)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return subject is null
            ? Result.Failure<SubjectDto>("Subject not found")
            : subject.Adapt<SubjectDto>().AsSuccessResult();
    }
}
