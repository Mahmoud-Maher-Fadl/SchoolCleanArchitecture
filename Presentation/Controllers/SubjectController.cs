using Application.Enums;
using Application.Subject.Commands.Create;
using Application.Subject.Commands.Delete;
using Application.Subject.Commands.Update;
using Application.Subject.Dto;
using Application.Subject.Queries.All;
using Application.Subject.Queries.Id;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolCleanArchitecture.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]

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
    
    [HttpGet("{id}")]
    public async Task<Result<SubjectDto>> GetById(string id)
    {
        return await _mediator.Send(new GetSubjectByIdQuery() { Id = id });
    }
    public SubjectController(IMediator mediator) : base(mediator)
    {
    }
}