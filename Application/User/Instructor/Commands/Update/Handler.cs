/*using Application.User.Instructor.Dto;
using Domain.common;
using Domain.Model.Instructor;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Type = Domain.Tenant.Type;

namespace Application.User.Instructor.Commands.Update;

public class Handler : IRequestHandler<UpdateInstructorCommand, Result<InstructorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IAdminContext _adminContext;
    private readonly UserManager<Domain.Tenant.Tenant> _userManager;

    public Handler(IApplicationDbContext context,
        UserManager<Domain.Tenant.Tenant> userManager, IAdminContext adminContext)
    {
        _context = context;
        _userManager = userManager;
        _adminContext = adminContext;
    }

    public async Task<Result<InstructorDto>> Handle(UpdateInstructorCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _adminContext.Tenants
            .Include(x => x.Instructor)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (user is null)
            return Result.Failure<InstructorDto>("Tenant Not Found");
        var isExistEmail = await _userManager.Users
            .Where(x=>x.Id!=request.Id)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (isExistEmail is not null)
            return Result.Failure<InstructorDto>($"This E-mail {request.Email} Is Already Used");

        var isExistUserName = await _userManager.Users
            .Where(x=>x.Id!=request.Id)
            .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
        if (isExistUserName is not null)
            return Result.Failure<InstructorDto>($"This Tenant Name {request.UserName} Is Already Used");

        if (request.DepartmentId is not null)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if (department is null)
                return Result.Failure<InstructorDto>("Department Not Found");
        }
        request.Adapt(user);
        user.Instructor = request.Adapt<Domain.Model.Instructor.Instructor>();
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded
            ? user.Adapt<InstructorDto>().AsSuccessResult()
            : Result.Failure<InstructorDto>(result.Errors.ToString() ?? string.Empty);
        
        
    }
}*/