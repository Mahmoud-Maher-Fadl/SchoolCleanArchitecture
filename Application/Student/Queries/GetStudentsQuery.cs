using Application.Common;
using Application.Student.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Student.Queries;

public class GetStudentsQuery:IRequest<Result<PagingList<StudentDto>>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public class Handler:IRequestHandler<GetStudentsQuery,Result<PagingList<StudentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagingList<StudentDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _context.Students
                .OrderBy(x=>x.Name)
                .Skip((request.Page -1)*request.PageSize)
                .Take(request.PageSize)
                .ProjectToType<StudentDto>()
                .ToListAsync(cancellationToken);
            return new PagingList<StudentDto>(students, request.Page, request.PageSize).AsSuccessResult();
        }
    }
}