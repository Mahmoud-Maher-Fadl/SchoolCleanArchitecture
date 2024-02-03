/*
using Application.Common;
using Application.Enums;
using Application.User.Student.Dto;
using Domain.common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Type = Domain.Tenant.Type;

namespace Application.User.Student.Queries.All;
public class Handler:IRequestHandler<GetStudentsQuery,Result<PagingList<StudentDto>>>
{
    private readonly IAdminContext _adminContext;

    public Handler(IAdminContext adminContext)
    {
        _adminContext = adminContext;
    }

    public async Task<Result<PagingList<StudentDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    { StudentsOrderingEnum orderByEnum = request.OrderBy;
        var filter = orderByEnum switch
        {
            StudentsOrderingEnum.Name=>"Name",
            StudentsOrderingEnum.Address=>"Address",
            StudentsOrderingEnum.DepartmentName=>"DepartmentName",
            StudentsOrderingEnum.EducationLevel=>"EducationLevel",
            _ => "CreateDate"
        };
        var students = await _adminContext.Tenants
            .Where(s=>s.UserName != null 
                      && (string.IsNullOrEmpty(request.Search) || s.UserName.Contains(request.Search))
                      && s.Type==Type.Student)
            .Include(x=>x.Student)
            .ThenInclude(x=>x.Department)
            .Include(x=>x.Student)
            .ThenInclude(x=>x.Subjects)
//            .OrderByDescending( d => EF.Property<object>(d, filter))
            .Skip((request.Page -1)*request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        return new PagingList<StudentDto>(students.Adapt<List<StudentDto>>(), request.Page, request.PageSize,request.Search,request.OrderBy).AsSuccessResult();
    }
}
*/
