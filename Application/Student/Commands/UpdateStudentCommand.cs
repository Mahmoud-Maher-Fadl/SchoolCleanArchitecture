using Application.Department.Commands;
using Application.Student.Dto;
using Domain.common;
using Domain.Model.Student;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Student.Commands;

public class UpdateStudentCommand:IRequest<Result<StudentDto>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string DepartmentId { get; set; }
    public List<string> SubjectsId { get; set; } = new List<string>();
    public class Validator:AbstractValidator<UpdateStudentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.Phone).NotEmpty();
            RuleFor(c => c.DepartmentId).NotEmpty();
        }
    }

    public class Handler:IRequestHandler<UpdateStudentCommand,Result<StudentDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentRepository _studentRepository;

        public Handler(ApplicationDbContext context, IStudentRepository studentRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
        }

        public async Task<Result<StudentDto>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FindAsync(request.Id);
            if(student is null)
                return Result.Failure<StudentDto>("Student not found");
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if(department is null)
                return Result.Failure<StudentDto>("Department not found");
            request.Adapt(student);
            student.Department = department;
            foreach (var subjectId in request.SubjectsId)
            {
                var subject = await _context.Subjects.FindAsync(subjectId);
                if (subject is null)
                    return Result.Failure<StudentDto>($"Invalid Subject Id :{subjectId}");
                student.Subjects.Add(subject);
            }

            var result = await _studentRepository.Update(student, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<StudentDto>().AsSuccessResult()
                : Result.Failure<StudentDto>(result.Error);


        }
    }
}