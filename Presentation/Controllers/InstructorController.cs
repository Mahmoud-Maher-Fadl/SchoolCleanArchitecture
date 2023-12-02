using Application.Instructor.Commands.Create;
using Application.Instructor.Commands.Delete;
using Application.Instructor.Commands.Update;
using Application.Instructor.Dto;
using Application.Instructor.Queries.All;
using Application.Instructor.Queries.Id;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace SchoolCleanArchitecture.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InstructorController : ApiController
{
    [HttpPost]
    [SwaggerRequestExample(typeof(CreateInstructorCommand), typeof(CreateInstructorCommand.Example))]

    public async Task<Result> Add(CreateInstructorCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SwaggerRequestExample(typeof(UpdateInstructorCommand), typeof(UpdateInstructorCommand.Example))]

    public async Task<Result> Update(UpdateInstructorCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpDelete]
    [SwaggerRequestExample(typeof(DeleteInstructorCommand), typeof(DeleteInstructorCommand.Example))]

    public async Task<Result> Delete(string Id)
    {
        return await _mediator.Send(new DeleteInstructorCommand(){Id = Id});
    }
    

    [HttpGet]
    public async Task<Result> GetAll()
    {
       
        return await _mediator.Send(new GetInstructorsQuery());
    }
    [HttpGet("{id}")]
    [SwaggerRequestExample(typeof(GetInstructorByIdQuery), typeof(GetInstructorByIdQuery.Example))]
    public async Task<Result<InstructorDto>> GetById(string id)
    {
        return await _mediator.Send(new GetInstructorByIdQuery() { Id = id });
    }
    public InstructorController(IMediator mediator) : base(mediator)
    {
    }
}