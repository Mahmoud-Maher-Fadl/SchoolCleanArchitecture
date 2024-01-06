using Application.Common;
using Application.Enums;
using Application.Student.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Student.Queries.All;

public class GetStudentsQuery:IRequest<Result<PagingList<StudentDto>>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }=String.Empty;
    public StudentsOrderingEnum OrderBy { get; set; }
}