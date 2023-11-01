using Application.Common;
using Application.Enums;
using Application.Subject.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Application.Subject.Queries;

public class GetSubjectsQuery:IRequest<Result<PagingList<SubjectDto>>>
{
    
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }
    public SubjectsOrderingEnum OrderBy { get; set; }

    public class Handler:IRequestHandler<GetSubjectsQuery,Result<PagingList<SubjectDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Result<PagingList<SubjectDto>>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
        {
            SubjectsOrderingEnum orderByEnum = request.OrderBy;
            var filter = orderByEnum switch
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
                //.Include(s=>s.Department)
                .ToListAsync(cancellationToken);
            return new PagingList<SubjectDto>(subjects, request.Page, request.PageSize,request.Search,request.OrderBy).AsSuccessResult();
        }
    }
}