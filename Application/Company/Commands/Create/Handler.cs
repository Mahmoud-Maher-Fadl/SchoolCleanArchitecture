using Application.Company.Dto;
using Application.Company.Notifications;
using Domain.common;
using Domain.Tenant;
using Mapster;
using MediatR;

namespace Application.Company.Commands.Create;

public class Handler : IRequestHandler<CreateCompanyCommand, Result<CompanyDto>>
{
    private readonly ISchoolRepository _schoolRepository;
    private readonly IMediator _mediator;

    public Handler(ISchoolRepository companyRepository, IMediator mediator)
    {
        _schoolRepository = companyRepository;
        _mediator = mediator;
    }

    public async Task<Result<CompanyDto>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var school = request.Adapt<School>();
        var result = await _schoolRepository.Add(school, cancellationToken);
        if (!result.IsSuccess)
            return Result.Failure<CompanyDto>(result.Error);
        await _mediator.Publish(new NewCompanyCreatedNotification(school), cancellationToken);
        return result.Value.Adapt<CompanyDto>().AsSuccessResult();
    }
}