using Application.Instructor.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Type = Domain.Identity.Type;

namespace Application.Instructor.Queries.All;
public class Handler:IRequestHandler<GetInstructorsQuery,Result<List<InstructorDto>>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<InstructorDto>>> Handle(GetInstructorsQuery request, CancellationToken cancellationToken)
    {
        var instructors = await _context.Users
            .Where(x=>x.Type==Type.Instructor)
            .Include(x=>x.Department)
            .Include(x=>x.Instructor)
            .ThenInclude(x=>x.Subjects)
            .ToListAsync(cancellationToken);
        return instructors.Adapt<List<InstructorDto>>().AsSuccessResult();

    }
}
