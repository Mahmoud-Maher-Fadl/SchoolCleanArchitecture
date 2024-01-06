using Domain.common;
using Domain.JWT;
using Domain.Role;
using Mapster;
using MediatR;

namespace Application.Role.Commands.Delete;

public class Handler:IRequestHandler<DeleteRoleCommand,Result<RoleDto>>
{
    private readonly IRoleRepo _roleRepo;

    public Handler(IRoleRepo roleRepo)
    {
        _roleRepo = roleRepo;
    }

    public async Task<Result<RoleDto>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleRepo.DeleteRole(request.Id);
        return result.Adapt<RoleDto>().AsSuccessResult();
    }
}