using Application.Subject.Commands;
using Application.Subject.Queries;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SubjectController : ApiController
{
    [HttpPost]
    public async Task<Result> Add(CreateSubjectCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpPut]
    public async Task<Result>Update(UpdateSubjectCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete]
    public async Task<Result>Delete(string id)
    {
        return await _mediator.Send(new DeleteSubjectCommand(){Id = id});
    }
    [HttpGet]
    public async Task<Result> GetAll()
    {
        return await _mediator.Send(new GetSubjectsQuery());
    }
    
    
    public SubjectController(IMediator mediator) : base(mediator)
    {
    }
}