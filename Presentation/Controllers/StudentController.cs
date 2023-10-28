using Application.Student.Commands;
using Application.Student.Queries;
using Domain.common;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Controllers;
[ApiController]
[Route("api/[controller]")]

public class StudentController : ApiController
{
    [HttpPost]
    public async Task<Result>Add(CreateStudentCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result>Update(UpdateStudentCommand command)
    {
        return await _mediator.Send(command);
    }
   [HttpDelete]
    public async Task<Result>Delete(string id)
    {
        return await _mediator.Send(new DeleteStudentCommand(){Id = id});
    }
    [HttpGet]
    public async Task<Result> GetAll( [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,[FromQuery] string search="",[FromQuery] StudentsOrderingEnum orderby=0 )
    {
        var query = new GetStudentsQuery()
        {
            Page = page,
            PageSize = pageSize,
            Search=search,
            OrderBy = orderby,
        };
        return await _mediator.Send(query);
    }
    [HttpGet("Search/")]
    public async Task<Result> GetBy(string value)
    {
        return await _mediator.Send(new GetStudentByPredQuery(){value = value});
    }
    public StudentController(IMediator mediator) : base(mediator)
    {
    }
}