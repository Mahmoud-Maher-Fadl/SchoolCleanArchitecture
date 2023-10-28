using Application.Common;
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
    public class Handler:IRequestHandler<GetSubjectsQuery,Result<PagingList<SubjectDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Result<PagingList<SubjectDto>>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _context.Subjects
                .OrderBy(x=>x.Name)
                .Skip((request.Page -1)*request.PageSize)
                .Take(request.PageSize)
                //.Include(s=>s.Department)
                .ProjectToType<SubjectDto>()
                .ToListAsync(cancellationToken);
            return new PagingList<SubjectDto>(subjects, request.Page, request.PageSize).AsSuccessResult();
        }
    }
}