using Application.Student.Dto;
using Domain.common;
using Domain.Model.Student;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Student.Commands.Update;

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
        var student = await _context.Students
            .FirstOrDefaultAsync(s=>s.Id == request.Id, cancellationToken);
        if(student is null)
            return Result.Failure<StudentDto>("Student not found");
        if (request.DepartmentId is not null)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if (department is null)
                return Result.Failure<StudentDto>("Department not found");
        }
        if (request.SubjectsId is not null)
        {
            student.Subjects?.Clear();
            foreach (var subjectId in request.SubjectsId)
            {
                if (subjectId.Length > 0)
                {
                    var subject = await _context.Subjects.FindAsync(subjectId);
                    if (subject is null)
                        return Result.Failure<StudentDto>($"Invalid Subject Id :{subjectId}");
                    student.Subjects?.Add(subject);
                }
            }
        }
        request.Adapt(student);
        var result = await _studentRepository.Update(student, cancellationToken);
        return result.IsSuccess
            ? result.Value.Adapt<StudentDto>().AsSuccessResult()
            : Result.Failure<StudentDto>(result.Error);


    }
}
