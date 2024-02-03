using Application.Company.Dto;
using Domain.common;
using FluentValidation;
using MediatR;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Company.Commands.Create;

public record CreateCompanyCommand(
    string ArabicName,
    string EnglishName
) : IRequest<Result<CompanyDto>>;

public class Validator : AbstractValidator<CreateCompanyCommand>
{
    public Validator()
    {
        RuleFor(x => x.ArabicName).NotEmpty();
        RuleFor(x => x.EnglishName).NotEmpty();
    }
}
public class Example : IExamplesProvider<CreateCompanyCommand>
{
    public CreateCompanyCommand GetExamples()
    {
        return new CreateCompanyCommand
        (
            ArabicName: "ArabicName",
            EnglishName: "EnglishName"
        );
    }
}