using Application.Subject.Dto;
using Domain.common;
using Domain.Model.Subject;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Subject.Commands;

public class CreateSubjectCommand:IRequest<Result<SubjectDto>>
{
    public string Name { get; set; }
    public int Hours { get; set; }
    public string? DepartmentId { get; set; }
    public class Validator:AbstractValidator<CreateSubjectCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Hours).NotEmpty();
        }
    }

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
            var subject = request.Adapt<Domain.Model.Subject.Subject>();
            if (request.DepartmentId is not null&&request.DepartmentId.Length>0)
            {
                var department = await _context.Departments.FindAsync(request.DepartmentId);
                if(department is null)
                    return Result.Failure<SubjectDto>("Invalid Department Id");
                subject.Department = department;
            }
            else
            {
                subject.DepartmentId = null;
            }
            var result = await _subjectRepository.Add(subject, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<SubjectDto>().AsSuccessResult()
                : Result.Failure<SubjectDto>(result.Error);
        }
    }
}