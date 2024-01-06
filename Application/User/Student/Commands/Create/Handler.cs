using Application.Student.Dto;
using Domain.common;
using Domain.Model.Student;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Type = Domain.Identity.Type;

namespace Application.User.Student.Commands.Create;

public class Handler:IRequestHandler<CreateStudentCommand,Result<StudentDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Domain.Identity.User> _userManager;

    public Handler(ApplicationDbContext context, UserManager<Domain.Identity.User> userManager)    
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<Result<StudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        
        var isExistEmail =await _userManager.FindByEmailAsync(request.Email);
        if (isExistEmail is not null)
            return Result.Failure<StudentDto>($"This E-mail {request.Email} Is Already Used");
           
        var isExistUserName = await _userManager.FindByNameAsync(request.UserName);
        if (isExistUserName is not null)
            return Result.Failure<StudentDto>($"This User Name {request.UserName} Is Already Used");
            
        if (request.DepartmentId is not null)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if (department is null)
                return Result.Failure<StudentDto>("Department Not Found"); 
        }
        
        if (request.SubjectsId is not null)
        {
            foreach (var subjectId in request.SubjectsId)
            {
                 var subject = await _context.Subjects.FindAsync(subjectId);
                    if (subject is null)
                        return Result.Failure<StudentDto>($"Invalid Subject Id :{subjectId} ");
            }
        }

        var user = request.Adapt<Domain.Identity.User>();
        user.Student = request.Adapt<Domain.Model.Student.Student>();
        var subjects = await _context.Subjects
            .Where(x => request.SubjectsId.Contains(x.Id))
            .ToListAsync(cancellationToken);
        user.Student.Subjects = subjects.ToHashSet();
        user.Type = Type.Student;
        
        var result = await _userManager.CreateAsync(user);
        var role = await _context.Roles.FindAsync(request.RoleId);
        if (role is null)
            return Result.Failure<StudentDto>($"Invalid Role Id :{request.RoleId} ");
        await _userManager.AddToRoleAsync(user, role.Name);
        return result.Succeeded
            ? Result.Success<StudentDto>(user.Adapt<StudentDto>()) 
            : Result.Failure<StudentDto>(result.Errors.ToString() ?? string.Empty);
    }
}
