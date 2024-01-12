using Application.User.Instructor.Commands.Create;
using Application.User.Instructor.Commands.Delete;
using Application.User.Instructor.Commands.Update;
using Application.User.Instructor.Dto;
using Application.User.Instructor.Queries.All;
using Application.User.Instructor.Queries.Id;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolCleanArchitecture.Controllers;
[ApiController]
[Route("api/[controller]")]
public class InstructorController : ApiController
{
    [HttpPost]

    public async Task<Result> Add(CreateInstructorCommand command)
    {
        return await _mediator.Send(command);
    }
    [Authorize]
    [HttpPut]

    public async Task<Result> Update(UpdateInstructorCommand command)
    {
        return await _mediator.Send(command);
    }
//    [Authorize]
    [HttpDelete]

    public async Task<Result> Delete(string Id)
    {
        return await _mediator.Send(new DeleteInstructorCommand(){Id = Id});
    }
    
    [Authorize]
    [HttpGet]
    public async Task<Result> GetAll()
    {
       
        return await _mediator.Send(new GetInstructorsQuery());
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<Result<InstructorDto>> GetById(string id)
    {
        return await _mediator.Send(new GetInstructorByIdQuery() { Id = id });
    }
    public InstructorController(IMediator mediator) : base(mediator)
    {
    }
}