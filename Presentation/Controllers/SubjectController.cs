using Application.Enums;
using Application.Subject.Commands;
using Application.Subject.Queries;
using Domain.common;
using Domain.Model.Subject;
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
    public async Task<Result> GetAll( [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,[FromQuery] string search="",[FromQuery] SubjectsOrderingEnum orderby=0 )
    {
        var query = new GetSubjectsQuery()
        {
            Page = page,
            PageSize = pageSize,
            Search = search,
            OrderBy = orderby
        };
        return await _mediator.Send(query);
    }
    
    
    public SubjectController(IMediator mediator) : base(mediator)
    {
    }
}