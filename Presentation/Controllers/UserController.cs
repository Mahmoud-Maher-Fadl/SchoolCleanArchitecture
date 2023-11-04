using Application.User.Commands;
using Application.User.Dto;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ApiController
{

    [HttpPost]
    public async Task<Result> Add(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }
    
    
    public UserController(IMediator mediator) : base(mediator)
    {
    }
}