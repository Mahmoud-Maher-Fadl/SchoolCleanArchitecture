using Application.Company.Dto;
using Domain.common;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Company.Commands.Update;

public record UpdateCompanyCommand(
    string Id,
    string ArabicName,
    string EnglishName
) : IRequest<Result<CompanyDto>>;

public class Validator : AbstractValidator<UpdateCompanyCommand>
{
    public Validator()
    {
        RuleFor(x => x.ArabicName).NotEmpty();
        RuleFor(x => x.EnglishName).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
    }
}
public class Example : IExamplesProvider<UpdateCompanyCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public Example(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public UpdateCompanyCommand GetExamples()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IAdminContext>();
        var company = context.Schools.FirstOrDefault();
        return company?.Adapt<UpdateCompanyCommand>() ?? new UpdateCompanyCommand("", "", "");
    }
}