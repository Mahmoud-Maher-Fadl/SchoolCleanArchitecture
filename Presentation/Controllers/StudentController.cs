using Application.Student.Commands;
using Application.Student.Queries;
using Domain.common;
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
    [HttpGet]
    public async Task<Result> GetAll()
    {
        return await _mediator.Send(new GetStudentsQuery());
    }
    public StudentController(IMediator mediator) : base(mediator)
    {
    }
}