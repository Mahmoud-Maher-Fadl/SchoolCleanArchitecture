using Domain.common;
using Domain.Role;
using Mapster;
using MediatR;

namespace Application.Role.Commands.Create;

public class Handler:IRequestHandler<CreateRoleCommand,Result<RoleDto>>
{
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