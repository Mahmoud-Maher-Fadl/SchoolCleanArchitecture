using Domain.common;
using Domain.JWT;
using Domain.Role;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Role.Queries.All;

public class Handler:IRequestHandler<GetRolesQuery,Result<List<RoleDto>>>
{
    private readonly IAdminContext _context;

    public Handler(IAdminContext context)
    {
        _context = context;
    }

    public async Task<Result<List<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _context.Roles
            .ToListAsync(cancellationToken);
        return roles.Adapt<List<RoleDto>>().AsSuccessResult();
    }
}