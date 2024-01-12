using Domain.common;
using Domain.Role;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Role.Queries.ByUserId;

public class Handler:IRequestHandler<GetUserRolesQuery,Result<List<RoleDto>>>
{
    private readonly UserManager<Domain.Identity.User> _userManager;

    public Handler(UserManager<Domain.Identity.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<List<RoleDto>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return Result.Failure<List<RoleDto>>("User Not Found");
        var roles = await _userManager.GetRolesAsync(user);
        return roles.Adapt<List<RoleDto>>().AsSuccessResult();
    }
}