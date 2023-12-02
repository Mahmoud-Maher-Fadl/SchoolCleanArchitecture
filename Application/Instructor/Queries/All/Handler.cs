using Application.Instructor.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var instructors = await _context.Instructors
            .ProjectToType<InstructorDto>()
            .ToListAsync(cancellationToken);
        return instructors.AsSuccessResult();

    }
}
