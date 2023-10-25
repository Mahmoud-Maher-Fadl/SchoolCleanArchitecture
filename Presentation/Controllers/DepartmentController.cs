using Application.Department.Commands;
using Application.Department.Queries;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Controllers;
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
    public async Task<Result> GetAll()
    {
        return await _mediator.Send(new GetDepartmentsQuery());
    }
    public DepartmentController(IMediator mediator) : base(mediator)
    {
    }
}