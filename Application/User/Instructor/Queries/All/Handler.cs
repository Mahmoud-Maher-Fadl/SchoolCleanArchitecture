/*
using Application.User.Instructor.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Type = Domain.Tenant.Type;

namespace Application.User.Instructor.Queries.All;
public class Handler:IRequestHandler<GetInstructorsQuery,Result<List<InstructorDto>>>
{
    private readonly IAdminContext _context;

    public Handler(IAdminContext context)
    {
        _context = context;
    }

    public async Task<Result<List<InstructorDto>>> Handle(GetInstructorsQuery request, CancellationToken cancellationToken)
    {
        var instructors = await _context.Tenants
            .Where(x=>x.Type==Type.Instructor)
            .Include(x=>x.Instructor)
            .ThenInclude(x=>x.Department)
            .Include(x=>x.Instructor)
            .ThenInclude(x=>x.Subjects)
            .ToListAsync(cancellationToken);
        return instructors.Adapt<List<InstructorDto>>().AsSuccessResult();

    }
}
*/
