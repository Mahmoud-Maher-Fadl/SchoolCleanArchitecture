using Application.User.Commands.Create;
using Application.User.Commands.Delete;
using Application.User.Commands.Update;
using Application.User.Commands.UserPassword.ChangeUserPassword;
using Application.User.Dto;
using Application.User.Queries.All;
using Application.User.Queries.Id;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

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
    [SwaggerRequestExample(typeof(CreateUserCommand), typeof(CreateUserCommand.Example))]

    public async Task<Result<UserDto>> Add(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SwaggerRequestExample(typeof(UpdateUserCommand), typeof(UpdateUserCommand.Example))]

    public async Task<Result>Update(UpdateUserCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete]
    [SwaggerRequestExample(typeof(DeleteUserCommand), typeof(DeleteUserCommand.Example))]

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
    [SwaggerRequestExample(typeof(GetUserByIdQuery), typeof(GetUserByIdQuery.Example))]

    public async Task<Result> GetById(string id)
    {
        return await _mediator.Send(new GetUserByIdQuery(){Id = id});
    }
    [HttpPatch]
    [SwaggerRequestExample(typeof(ChangeUserPasswordCommand), typeof(ChangeUserPasswordCommand.Example))]

    public async Task<Result> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await _mediator.Send(command);
    }
}