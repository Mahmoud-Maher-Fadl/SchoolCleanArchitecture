using Application.Enums;
using Application.Student.Commands.Delete;
using Application.Student.Commands.Update;
using Application.Student.Dto;
using Application.Student.Queries;
using Application.Student.Queries.All;
using Application.Student.Queries.Id;
using Application.User.Student.Commands.Create;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace SchoolCleanArchitecture.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentController : ApiController
{
    [HttpPost]
    public async Task<Result> Add(CreateStudentCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    public async Task<Result> Update(UpdateStudentCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete]
    public async Task<Result> Delete(string id)
    {
        return await _mediator.Send(new DeleteStudentCommand() { Id = id });
    }

    [HttpGet]
    public async Task<Result> GetAll([FromQuery] int page = 1,
        [FromQuery] int pageSize = 10, [FromQuery] string search = "", [FromQuery] StudentsOrderingEnum orderby = 0)
    {
        var query = new GetStudentsQuery()
        {
            Page = page,
            PageSize = pageSize,
            Search = search,
            OrderBy = orderby,
        };
        return await _mediator.Send(query);
    }

    [HttpGet("Search/")]
    public async Task<Result> GetBy(string value)
    {
        return await _mediator.Send(new GetStudentByPredQuery() { value = value });
    }

    [HttpGet("{id}")]
    public async Task<Result<StudentDto>> GetById(string id)
    {
        return await _mediator.Send(new GetStudentByIdQuery() { Id = id });
    }

    public StudentController(IMediator mediator) : base(mediator)
    {
    }
}