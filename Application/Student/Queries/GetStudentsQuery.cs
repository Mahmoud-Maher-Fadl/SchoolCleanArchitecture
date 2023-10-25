using Application.Student.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Student.Queries;

public class GetStudentsQuery:IRequest<Result<List<StudentDto>>>
{
    public class Handler:IRequestHandler<GetStudentsQuery,Result<List<StudentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<StudentDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _context.Students
                .ProjectToType<StudentDto>()
                .ToListAsync(cancellationToken);
            return students.AsSuccessResult();
        }
    }
}