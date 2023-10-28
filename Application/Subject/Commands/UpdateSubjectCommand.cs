using Application.Subject.Dto;
using Domain.common;
using Domain.Model.Subject;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subject.Commands;

public class UpdateSubjectCommand:IRequest<Result<SubjectDto>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Hours { get; set; }
    public string? DepartmentId { get; set; }
    public class Validator:AbstractValidator<UpdateSubjectCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Hours).NotEmpty();
        }
    }

    public class Handler:IRequestHandler<UpdateSubjectCommand,Result<SubjectDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISubjectRepository _subjectRepository;

        public Handler(ApplicationDbContext context, ISubjectRepository subjectRepository)
        {
            _context = context;
            _subjectRepository = subjectRepository;
        }

        public async Task<Result<SubjectDto>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects.Include(s=>s.Department).Include(s=>s.Students).FirstOrDefaultAsync(s=>s.Id==request.Id);
            if (subject is null)
                return Result.Failure<SubjectDto>("Subject Not Found");
            request.Adapt(subject);
            if (request.DepartmentId is not null&&request.DepartmentId.Length > 0)
            {
                var department = await _context.Departments.FindAsync(request.DepartmentId); // After this
                                                                                             // Subject.Department will be assigned to department
                if (department is null)
                {
                    subject.DepartmentId = null;
                    return Result.Failure<SubjectDto>("Invalid Department ID");
                }
                subject.Department = department;
            }
            else
            {
                subject.DepartmentId = null;
            }
            var result = await _subjectRepository.Update(subject, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<SubjectDto>().AsSuccessResult()
                : Result.Failure<SubjectDto>(result.Error);
        }
    }
}