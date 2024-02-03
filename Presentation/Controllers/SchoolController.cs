using Application.Company.Commands.Create;
using Application.Company.Commands.Delete;
using Application.Company.Commands.Update;
using Application.Company.Dto;
using Application.Company.Queries.All;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SchoolCleanArchitecture.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchoolController : ApiController
{
    [HttpPost]
    public async Task<Result<CompanyDto>> Create(CreateCompanyCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    public async Task<Result<CompanyDto>> Update(UpdateCompanyCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpDelete("{id}")]
    public async Task<Result<CompanyDto>> Delete(string id)
    {
        return await _mediator.Send(new DeleteCompanyCommand(id));
    }
    
    [HttpGet]
    public async Task<Result<List<CompanyDto>>> GetAll()
    {
        return await _mediator.Send(new GetCompaniesQuery());
    }

    public SchoolController(IMediator mediator) : base(mediator)
    {
    }
}