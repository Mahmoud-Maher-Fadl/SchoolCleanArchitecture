using Domain.common;
using Domain.Role;
using MediatR;

namespace Application.Role.Queries.ByUserId;

public class GetUserRolesQuery:IRequest<Result<List<RoleDto>>>
{
    public string UserId { get; set; }
}