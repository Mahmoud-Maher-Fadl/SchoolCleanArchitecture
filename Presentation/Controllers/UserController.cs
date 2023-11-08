using Application.User.Commands;
using Application.User.Dto;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SchoolCleanArchitecture.Controllers;

[Route("api/[controller]")]
[ApiController]
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
}