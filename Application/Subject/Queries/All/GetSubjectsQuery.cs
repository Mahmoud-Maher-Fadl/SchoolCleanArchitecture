using Application.Common;
using Application.Enums;
using Application.Subject.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Application.Subject.Queries.All;

public class GetSubjectsQuery:IRequest<Result<PagingList<SubjectDto>>>
{
    
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }
    public SubjectsOrderingEnum OrderBy { get; set; }
}