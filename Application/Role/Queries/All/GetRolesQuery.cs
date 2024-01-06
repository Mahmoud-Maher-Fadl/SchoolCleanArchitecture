using Domain.common;
using Domain.JWT;
using Domain.Role;
using MediatR;

namespace Application.Role.Queries.All;

public class GetRolesQuery:IRequest<Result<List<RoleDto>>>
{
    
}