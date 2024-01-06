using Domain.common;
using Domain.JWT;
using Domain.Role;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Role.Queries.Id;

public class Handler:IRequestHandler<GetRoleByIdQuery,Result<RoleDto>>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public Handler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return role is null
            ? Result.Failure<RoleDto>("Role Doesn't Exist")
            : role.Adapt<RoleDto>().AsSuccessResult();
    }
}