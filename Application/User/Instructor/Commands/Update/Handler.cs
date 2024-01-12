using Application.User.Instructor.Dto;
using Domain.common;
using Domain.Model.Instructor;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Type = Domain.Identity.Type;

namespace Application.User.Instructor.Commands.Update;

public class Handler : IRequestHandler<UpdateInstructorCommand, Result<InstructorDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IInstructorRepository _instructorRepository;
    private readonly UserManager<Domain.Identity.User> _userManager;

    public Handler(ApplicationDbContext context, IInstructorRepository instructorRepository,
        UserManager<Domain.Identity.User> userManager)
    {
        _context = context;
        _instructorRepository = instructorRepository;
        _userManager = userManager;
    }

    public async Task<Result<InstructorDto>> Handle(UpdateInstructorCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.Instructor)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (user is null)
            return Result.Failure<InstructorDto>("User Not Found");
        var isExistEmail = await _userManager.Users
            .Where(x=>x.Id!=request.Id)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (isExistEmail is not null)
            return Result.Failure<InstructorDto>($"This E-mail {request.Email} Is Already Used");

        var isExistUserName = await _userManager.Users
            .Where(x=>x.Id!=request.Id)
            .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
        if (isExistUserName is not null)
            return Result.Failure<InstructorDto>($"This User Name {request.UserName} Is Already Used");

        if (request.DepartmentId is not null)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if (department is null)
                return Result.Failure<InstructorDto>("Department Not Found");
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var removeRolesResult= await _userManager.RemoveFromRolesAsync(user, userRoles);
        if(!removeRolesResult.Succeeded)
            return Result.Failure<InstructorDto>(removeRolesResult.Errors.Select(x=>x.Description).ToArray());
        if (request.Roles is { Length: > 0 })
        {
            foreach (var roleId in request.Roles)
            {
                var role = await _context.Roles.FindAsync(roleId);
                if (role is null)
                    return Result.Failure<InstructorDto>($"Invalid Role Id :{roleId} ");
            }

            var roles = await _context.Roles.Where(x => request.Roles
                .Contains(x.Id)).ToArrayAsync(cancellationToken);
            var rolesResult = await _userManager.AddToRolesAsync(user, roles.Select(x => x.Name)!);
            if(!rolesResult.Succeeded) 
                return Result.Failure<InstructorDto>(rolesResult.Errors.ToString() ?? string.Empty);
        }
        request.Adapt(user);
        user.Instructor = request.Adapt<Domain.Model.Instructor.Instructor>();
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded
            ? user.Adapt<InstructorDto>().AsSuccessResult()
            : Result.Failure<InstructorDto>(result.Errors.ToString() ?? string.Empty);
        
        
    }
}