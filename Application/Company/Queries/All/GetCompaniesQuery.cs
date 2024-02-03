using Application.Company.Dto;
using Domain.common;
using MediatR;

namespace Application.Company.Queries.All;

public record GetCompaniesQuery : IRequest<Result<List<CompanyDto>>>;