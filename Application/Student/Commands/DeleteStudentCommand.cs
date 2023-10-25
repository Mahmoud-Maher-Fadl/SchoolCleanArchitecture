using Application.Department.Dto;
using Application.Student.Dto;
using Domain.common;
using Domain.Model.Student;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Student.Commands;

public class DeleteStudentCommand:IRequest<Result<StudentDto>>
{
    public string Id { get; set; }
    public class Handler:IRequestHandler<DeleteStudentCommand,Result<StudentDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentRepository _studentRepository;

        public Handler(IStudentRepository studentRepository, ApplicationDbContext context)
        {
            _studentRepository = studentRepository;
            _context = context;
        }

        public async Task<Result<StudentDto>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FindAsync(request.Id);
            if (student is null)
                return Result.Failure<StudentDto>("Student Not Found");
            var result = await _studentRepository.Delete(student, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<StudentDto>().AsSuccessResult()
                : Result.Failure<StudentDto>(result.Error);
        }
    }
}