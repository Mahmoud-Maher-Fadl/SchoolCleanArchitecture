using Application.Authentication.Commands;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SchoolCleanArchitecture.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ApiController
{

    [HttpPost]
    public async Task<Result> Create(SignInCommand command)
    {
        return await _mediator.Send(command);
    }
    
    public AuthController(IMediator mediator) : base(mediator)
    {
    }
}