using Application.Common;
using Application.Department.Dto;
using Domain.common;
using Application.Enums;
using Domain.Model.Department;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Department.Queries.All;

public class GetDepartmentsQuery:IRequest<Result<PagingList<DepartmentDto>>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }  
    public string Search { get; set; }=String.Empty;
    public DepartmentsOrderingEnum OrderBy { get; set; }
}