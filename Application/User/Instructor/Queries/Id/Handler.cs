/*
using Application.User.Instructor.Dto;
using Domain.common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Instructor.Queries.Id;
public class Handler:IRequestHandler<GetInstructorByIdQuery,Result<InstructorDto>>
{
    private readonly IAdminContext _context;

    public Handler(IAdminContext context)
    {
        _context = context;
    }

    public async Task<Result<InstructorDto>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
    {
        var instructor = await _context.Tenants
            .Include(x => x.Instructor)
            .ThenInclude(x=>x.Department)
            .Include(x=>x.Instructor)
            .ThenInclude(x=>x.Subjects)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return instructor is null
            ? Result.Failure<InstructorDto>("Instructor not found")
            : instructor.Adapt<InstructorDto>().AsSuccessResult();
    }
}
*/
