using Application.Department.Commands.Create;
using Application.Department.Commands.Delete;
using Application.Department.Commands.Update;
using Application.Department.Dto;
using Application.Department.Queries.All;
using Application.Department.Queries.Id;
using Application.Enums;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolCleanArchitecture.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartmentController : ApiController
{
    [HttpPost]

    public async Task<Result> Add(CreateDepartmentCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]

    public async Task<Result> Update(UpdateDepartmentCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpDelete]
    public async Task<Result> Delete(string Id)
    {
        return await _mediator.Send(new DeleteDepartmentCommand(){Id = Id});
    }
    

    [HttpGet]
    public async Task<Result> GetAll( [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,[FromQuery] string search="",[FromQuery] DepartmentsOrderingEnum orderby=0 )
    {
        var query = new GetDepartmentsQuery()
        {
            Page = page,
            PageSize = pageSize,
            Search = search,
            OrderBy = orderby,
        };
        return await _mediator.Send(query);
    }
    
    [HttpGet("{id}")]
    public async Task<Result<DepartmentDto>> GetById(string id)
    {
        return await _mediator.Send(new GetDepartmentByIdQuery() { Id = id });
    }
    public DepartmentController(IMediator mediator) : base(mediator)
    {
    }
}