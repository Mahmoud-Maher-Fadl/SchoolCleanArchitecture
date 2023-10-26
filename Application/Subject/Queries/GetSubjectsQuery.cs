using Application.Subject.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subject.Queries;

public class GetSubjectsQuery:IRequest<Result<List<SubjectDto>>>
{
    
    public class Handler:IRequestHandler<GetSubjectsQuery,Result<List<SubjectDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Result<List<SubjectDto>>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _context.Subjects
                //.Include(s=>s.Department)
                .ProjectToType<SubjectDto>()
                .ToListAsync(cancellationToken);
            return subjects.AsSuccessResult();
        }
    }
}