using Application.Company.Dto;
using Domain.common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Company.Commands.Delete;

public record DeleteCompanyCommand(string Id) : IRequest<Result<CompanyDto>>;

public class Validator : AbstractValidator<DeleteCompanyCommand>
{
    public Validator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
public class Example : IExamplesProvider<DeleteCompanyCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public Example(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public DeleteCompanyCommand GetExamples()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IAdminContext>();
        var company = context.Schools.Select(x => x.Id).FirstOrDefault();
        return new DeleteCompanyCommand(company ?? string.Empty);
    }
}