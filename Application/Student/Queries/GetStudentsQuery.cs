using Application.Common;
using Application.Student.Dto;
using Domain.common;
using Domain.Enums;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Student.Queries;

public class GetStudentsQuery:IRequest<Result<PagingList<StudentDto>>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Search { get; set; }
    public StudentsOrderingEnum? OrderBy { get; set; }

    public class Handler:IRequestHandler<GetStudentsQuery,Result<PagingList<StudentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagingList<StudentDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            StudentsOrderingEnum orderByEnum = request.OrderBy ?? StudentsOrderingEnum.Id;
            var filter = orderByEnum switch
            {
                StudentsOrderingEnum.Id=>"Id",
                StudentsOrderingEnum.Name=>"Name",
                StudentsOrderingEnum.Address=>"Address",
                StudentsOrderingEnum.DepartmentName=>"DepartmentName",
                StudentsOrderingEnum.EducationLevel=>"EducationLevel",
                _ => "Id"
            };
            var students = await _context.Students
                .Where(s=>s.Name.Contains(request.Search ?? string.Empty))
                .OrderBy(( d) => EF.Property<object>(d, filter))
                .Skip((request.Page -1)*request.PageSize)
                .Take(request.PageSize)
                .ProjectToType<StudentDto>()
                .ToListAsync(cancellationToken);
            return new PagingList<StudentDto>(students, request.Page, request.PageSize,request.Search,request.OrderBy).AsSuccessResult();
        }
    }
}