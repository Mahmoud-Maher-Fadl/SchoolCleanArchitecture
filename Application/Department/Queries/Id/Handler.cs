using Application.Department.Dto;
using Domain.common;
using Domain.Model.Instructor;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Department.Queries.Id;

public class Handler:IRequestHandler<GetDepartmentByIdQuery,Result<DepartmentDto>>
{
    private readonly IApplicationDbContext _context;

    public Handler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments
            .Include(x => x.Students)
            .Include(x=>x.Instructors)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return department is null
            ? Result.Failure<DepartmentDto>("Department Not Found")
            : department.Adapt<DepartmentDto>().AsSuccessResult();
    }
}
