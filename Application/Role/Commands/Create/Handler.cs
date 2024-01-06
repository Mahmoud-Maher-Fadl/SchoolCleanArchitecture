using System.Diagnostics;
using Domain.common;
using Domain.JWT;
using Domain.Role;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace Application.Role.Commands.Create;

public class Handler:IRequestHandler<CreateRoleCommand,Result<RoleDto>>
{
    //private readonly RoleManager<Domain.Identity.User> _roleManager;
    //private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IRoleRepo _roleRepo;

    public Handler(IRoleRepo roleRepo)
    {
        _roleRepo = roleRepo;
    }

    public async Task<Result<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleRepo.GenerateRole(request.RoleName);
        return result.Adapt<RoleDto>().AsSuccessResult();
    }
}