using Application.Localization;
using Application.User.Student.Dto;
using Domain.common;
using Domain.Model.Student;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.User.Student.Commands.Delete;

public class Handler:IRequestHandler<DeleteStudentCommand,Result<StudentDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IStudentRepository _studentRepository;
        
    private readonly IStringLocalizer<SharedResources> _stringlocalizer;


    public Handler(IStudentRepository studentRepository, ApplicationDbContext context, IStringLocalizer<SharedResources> stringlocalizer)
    {
        _studentRepository = studentRepository;
        _context = context;
        _stringlocalizer = stringlocalizer;
    }

    public async Task<Result<StudentDto>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students.FindAsync(request.Id);
        if (student is null)
            //return Result.Failure<StudentDto>("Student Not Found");
            return Result.Failure<StudentDto>(_stringlocalizer[SharedResourcesKeys.NotFound]);
        var result = await _studentRepository.Delete(student, cancellationToken);
        return result.IsSuccess
            ? result.Value.Adapt<StudentDto>().AsSuccessResult()
            : Result.Failure<StudentDto>(result.Error);
    }
}
