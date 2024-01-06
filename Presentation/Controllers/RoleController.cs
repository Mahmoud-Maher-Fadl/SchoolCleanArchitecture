using Application.Role.Commands.Create;
using Application.Role.Commands.Delete;
using Application.Role.Commands.Update;
using Application.Role.Queries.All;
using Application.Role.Queries.Id;
using Domain.common;
using Domain.JWT;
using Domain.Role;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolCleanArchitecture.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoleController : ApiController
{
    [HttpPost]
    public async Task<Result<RoleDto>> Create(CreateRoleCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]

    public async Task<Result>Update(UpdateRolCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete]
    public async Task<Result>Delete(string id)
    {
        return await _mediator.Send(new DeleteRoleCommand(){Id = id});
    } 
    [HttpGet]
    public async Task<Result>GetAll()
    {
        return await _mediator.Send(new GetRolesQuery());
    }
    
    [HttpGet("{id}")]
    public async Task<Result<RoleDto>> GetById(string id)
    {
        return await _mediator.Send(new GetRoleByIdQuery() { Id = id });
    }
    public RoleController(IMediator mediator) : base(mediator)
    {
    }
}