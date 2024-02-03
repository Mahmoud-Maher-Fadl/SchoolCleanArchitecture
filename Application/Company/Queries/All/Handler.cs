using Application.Company.Dto;
using Domain.common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Company.Queries.All;

public class Handler : IRequestHandler<GetCompaniesQuery, Result<List<CompanyDto>>>
{
    private readonly IAdminContext _adminContext;

    public Handler(IAdminContext adminContext)
    {
        _adminContext = adminContext;
    }

    public async Task<Result<List<CompanyDto>>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _adminContext.Schools.ToListAsync(cancellationToken);
        return companies.Adapt<List<CompanyDto>>().AsSuccessResult();
    }
}