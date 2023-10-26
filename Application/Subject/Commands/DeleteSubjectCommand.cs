using Application.Subject.Dto;
using Domain.common;
using Domain.Model.Subject;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subject.Commands;

public class DeleteSubjectCommand:IRequest<Result<SubjectDto>>
{
    public string Id { get; set; }
    public class Handler:IRequestHandler<DeleteSubjectCommand,Result<SubjectDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISubjectRepository _subjectRepository;

        public Handler(ApplicationDbContext context, ISubjectRepository subjectRepository)
        {
            _context = context;
            _subjectRepository = subjectRepository;
        }

        public async Task<Result<SubjectDto>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects.Include(s=>s.Students).FirstOrDefaultAsync(s=>s.Id==request.Id,cancellationToken);
            if(subject is null)
                return Result.Failure<SubjectDto>("Subject Not Found");
            var result = await _subjectRepository.Delete(subject, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<SubjectDto>().AsSuccessResult()
                : Result.Failure<SubjectDto>(result.Error);
        }
    }
}