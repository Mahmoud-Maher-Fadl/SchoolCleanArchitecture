using Application.Company.Dto;
using Domain.common;
using Domain.Tenant;
using Mapster;
using MediatR;

namespace Application.Company.Commands.Delete;

public class Handler : IRequestHandler<DeleteCompanyCommand, Result<CompanyDto>>
{
    private readonly ISchoolRepository _companyRepository;
    private readonly IMigrationService _migrationService;

    public Handler(ISchoolRepository companyRepository, IMigrationService migrationService)
    {
        _companyRepository = companyRepository;
        _migrationService = migrationService;
    }

    public async Task<Result<CompanyDto>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var result = await _companyRepository.DeleteById(request.Id, cancellationToken);
        if (!result.IsSuccess)
            return Result.Failure<CompanyDto>(result.Error);
        await _migrationService.DeleteDatabase(request.Id, cancellationToken);
        return result.Value.Adapt<CompanyDto>().AsSuccessResult();
    }
}