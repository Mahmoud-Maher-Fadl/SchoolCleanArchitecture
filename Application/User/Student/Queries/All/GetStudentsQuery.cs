using Application.Common;
using Application.Enums;
using Application.User.Student.Dto;
using Domain.common;
using MediatR;

namespace Application.User.Student.Queries.All;

public class GetStudentsQuery:IRequest<Result<PagingList<StudentDto>>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }=String.Empty;
    public StudentsOrderingEnum OrderBy { get; set; }
}