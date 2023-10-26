using Application.Student.Dto;
using Domain.common;
using Domain.Model.Student;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Student.Commands;

public class CreateStudentCommand:IRequest<Result<StudentDto>>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string? DepartmentId { get; set; }
    public List<string>? SubjectsId { get; set; } = new List<string>();
    public class Validator:AbstractValidator<CreateStudentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.Phone).NotEmpty();
        }
    }

    public class Handler:IRequestHandler<CreateStudentCommand,Result<StudentDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentRepository _studentRepository;

        public Handler(IStudentRepository studentRepository, ApplicationDbContext context)
        {
            _studentRepository = studentRepository;
            _context = context;
        }

        public async Task<Result<StudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = request.Adapt<Domain.Model.Student.Student>();
            if (request.DepartmentId is not null &&request.DepartmentId.Length > 0)
            {
                var department = await _context.Departments.FindAsync(request.DepartmentId);
                if (department is null)
                    return Result.Failure<StudentDto>("Invalid Department Id");
                //student.Department = department; // UnNecessary if i won't include th dept in the  _context.Students.firstordefault
            }
            else
            {
                student.DepartmentId = null;
            }
            if (request.SubjectsId is not null)
                foreach (var subjectId in request.SubjectsId)
                {
                    if (subjectId.Length>0)
                    {
                        var subject = await _context.Subjects.FindAsync(subjectId);
                        if (subject is null)
                            return Result.Failure<StudentDto>($"Invalid Subject Id :{subjectId} ");
                        student.Subjects.Add(subject);
                    }
                }


            var result = await _studentRepository.Add(student,cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<StudentDto>().AsSuccessResult()
                : Result.Failure<StudentDto>(result.Error);
        }
    }
}