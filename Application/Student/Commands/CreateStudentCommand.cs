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
    public string DepartmentId { get; set; }
    public List<string> SubjectsId { get; set; }
    public class Validator:AbstractValidator<CreateStudentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.Phone).NotEmpty();
            RuleFor(c => c.DepartmentId).NotEmpty();
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
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if (department is null)
                return Result.Failure<StudentDto>("Invalid Department Id");
            var student = request.Adapt<Domain.Model.Student.Student>();
            student.Department = department;
            foreach (var subjectId in request.SubjectsId)
            {
                var subject =await _context.Subjects.FindAsync(subjectId);
                if (subject is null)
                    return Result.Failure<StudentDto>($"Invalid Subject Id :{subjectId} ");
                student.Subjects.Add(subject);
            }
            var result = await _studentRepository.Add(student,cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<StudentDto>().AsSuccessResult()
                : Result.Failure<StudentDto>(result.Error);
        }
    }
}