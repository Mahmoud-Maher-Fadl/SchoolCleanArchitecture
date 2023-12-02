using Application.Common;
using Application.Enums;
using Application.Student.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Student.Queries.All;
public class Handler:IRequestHandler<GetStudentsQuery,Result<PagingList<StudentDto>>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagingList<StudentDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        StudentsOrderingEnum orderByEnum = request.OrderBy;
        var filter = orderByEnum switch
        {
            StudentsOrderingEnum.Name=>"Name",
            StudentsOrderingEnum.Address=>"Address",
            StudentsOrderingEnum.DepartmentName=>"DepartmentName",
            StudentsOrderingEnum.EducationLevel=>"EducationLevel",
            _ => "CreateDate"
        };
        var students = await _context.Students
            .Where(s=>s.Name.Contains(request.Search))
            .ProjectToType<StudentDto>()
            .OrderByDescending( d => EF.Property<object>(d, filter))
            .Skip((request.Page -1)*request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        return new PagingList<StudentDto>(students, request.Page, request.PageSize,request.Search,request.OrderBy).AsSuccessResult();
    }
}
