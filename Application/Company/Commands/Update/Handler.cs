using Application.Company.Dto;
using Domain.common;
using Domain.Tenant;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Company.Commands.Update;

public class Handler : IRequestHandler<UpdateCompanyCommand, Result<CompanyDto>>
{
    private readonly ISchoolRepository _companyRepository;
    private readonly IAdminContext _adminContext;

    public Handler(ISchoolRepository companyRepository, IAdminContext adminContext)
    {
        _companyRepository = companyRepository;
        _adminContext = adminContext;
    }

    public async Task<Result<CompanyDto>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _adminContext.Schools.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (company == null)
            return Result.Failure<CompanyDto>("Company not found");
        request.Adapt(company);
        var result = await _companyRepository.Update(company, cancellationToken);
        return result.IsSuccess
            ? result.Value.Adapt<CompanyDto>().AsSuccessResult()
            : Result.Failure<CompanyDto>(result.Error);
    }
}