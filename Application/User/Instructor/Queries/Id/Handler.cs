using Application.Instructor.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Instructor.Queries.Id;
public class Handler:IRequestHandler<GetInstructorByIdQuery,Result<InstructorDto>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<InstructorDto>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
    {
        var instructor = await _context.Users
            .Include(x => x.Instructor)
            .ThenInclude(x=>x.Subjects)
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return instructor is null
            ? Result.Failure<InstructorDto>("Instructor not found")
            : instructor.Adapt<InstructorDto>().AsSuccessResult();
    }
}
