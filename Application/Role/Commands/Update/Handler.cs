using Domain.common;
using Domain.JWT;
using Domain.Role;
using Mapster;
using MediatR;

namespace Application.Role.Commands.Update;

public class Handler:IRequestHandler<UpdateRolCommand,Result<RoleDto>>
{
    private readonly IRoleRepo _roleRepo;

    public Handler(IRoleRepo roleRepo)
    {
        _roleRepo = roleRepo;
    }

    public async Task<Result<RoleDto>> Handle(UpdateRolCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleRepo.UpdateRole(request.Id,request.RoleName);
        return result.Adapt<RoleDto>().AsSuccessResult();
    }
}