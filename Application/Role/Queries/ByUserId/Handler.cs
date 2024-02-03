using Domain.common;
using Domain.Role;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Role.Queries.ByUserId;

public class Handler:IRequestHandler<GetUserRolesQuery,Result<List<RoleDto>>>
{
    private readonly UserManager<Domain.Tenant.Tenant> _userManager;

    public Handler(UserManager<Domain.Tenant.Tenant> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<List<RoleDto>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return Result.Failure<List<RoleDto>>("Tenant Not Found");
        var roles = await _userManager.GetRolesAsync(user);
        return roles.Adapt<List<RoleDto>>().AsSuccessResult();
    }
}