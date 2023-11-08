using Application.Department.Commands;
using Application.Department.Queries;
using Application.Enums;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SchoolCleanArchitecture.Controllers;
[ApiController]
[Route("api/[controller]")]
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
    public DepartmentController(IMediator mediator) : base(mediator)
    {
    }
}