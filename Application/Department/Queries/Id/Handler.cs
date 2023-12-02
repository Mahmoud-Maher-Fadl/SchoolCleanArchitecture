using Application.Department.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Department.Queries.Id;

public class Handler:IRequestHandler<GetDepartmentByIdQuery,Result<DepartmentDto>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments
            .Include(x => x.Instructors)
            .Include(x => x.Students)
            .Include(x => x.Subjects)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return department is null
            ? Result.Failure<DepartmentDto>("Department Not Found")
            : department.Adapt<DepartmentDto>().AsSuccessResult();
    }
}
