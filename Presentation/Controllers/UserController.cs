using Application.User.Commands;
using Application.User.Dto;
using Application.User.Queries;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolCleanArchitecture.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]

public class UserController : ControllerBase
{

    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Result<UserDto>> Add(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result>Update(UpdateUserCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete]
    public async Task<Result>Delete(string id)
    {
        return await _mediator.Send(new DeleteUserCommand(){Id = id});
    }

    [HttpGet]
    public async Task<Result> GetAll()
    {
        return await _mediator.Send(new GetUsersQuery());
    }
    [HttpGet("{id}")]
    public async Task<Result> GetById(string id)
    {
        return await _mediator.Send(new GetUserByIdQuery(){Id = id});
    }
    [HttpPatch]
    public async Task<Result> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await _mediator.Send(command);
    }
}