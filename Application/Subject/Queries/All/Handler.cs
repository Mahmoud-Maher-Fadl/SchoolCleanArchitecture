using Application.Common;
using Application.Enums;
using Application.Subject.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subject.Queries.All;

public class Handler:IRequestHandler<GetSubjectsQuery,Result<PagingList<SubjectDto>>>
{
    private readonly IApplicationDbContext _context;

    public Handler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Result<PagingList<SubjectDto>>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        var filter =  request.OrderBy switch
        {
            SubjectsOrderingEnum.Name=>"Name",
            SubjectsOrderingEnum.DepartmentName=>"DepartmentName",
            _ => "CreateDate"
        };
        var subjects = await _context.Subjects
            .Where(s=>s.Name.Contains(request.Search))
            .ProjectToType<SubjectDto>()
            .OrderBy(d => EF.Property<object>(d, filter) == null ? 1 : 0) // Rows with null values will be at the end
            .Skip((request.Page -1)*request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        return new PagingList<SubjectDto>(subjects, request.Page, request.PageSize,request.Search,request.OrderBy).AsSuccessResult();
    }
}
