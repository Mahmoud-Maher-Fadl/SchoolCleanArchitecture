/*
using Application.User.Student.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Student.Commands.Update;

public class Handler:IRequestHandler<UpdateStudentCommand,Result<StudentDto>>
{
    private readonly IAdminContext _adminContext;
    private readonly IApplicationDbContext _context;
    private readonly UserManager<Domain.Tenant.Tenant> _userManager;

    public Handler(IApplicationDbContext context, UserManager<Domain.Tenant.Tenant> userManager, IAdminContext adminContext)
    {
        _context = context;
        _userManager = userManager;
        _adminContext = adminContext;
    }

    public async Task<Result<StudentDto>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _adminContext.Tenants
            .Include(user => user.Student)
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
            student.Student.Subjects?.Clear();
            foreach (var subjectId in request.SubjectsId)
            {
                if (subjectId.Length > 0)
                {
                    var subject = await _context.Subjects.FindAsync(subjectId);
                    if (subject is null)
                        return Result.Failure<StudentDto>($"Invalid Subject Id :{subjectId}");
                    student.Student.Subjects?.Add(subject);
                }
            }
        }
        request.Adapt(student);
        var result = await _userManager.UpdateAsync(student);
        return result.Succeeded
            ? Result.Success<StudentDto>(student.Adapt<StudentDto>())
            : Result.Failure<StudentDto>(result.Errors.ToString() ?? string.Empty);


    }
}
*/
