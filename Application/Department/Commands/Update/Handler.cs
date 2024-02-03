using Application.Department.Dto;
using Domain.common;
using Domain.Model.Department;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Department.Commands.Update;

public class Handler:IRequestHandler<UpdateDepartmentCommand,Result<DepartmentDto>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IApplicationDbContext _context;

    public Handler(IDepartmentRepository departmentRepository, IApplicationDbContext context)
    {
        _departmentRepository = departmentRepository;
        _context = context;
    }

    public async Task<Result<DepartmentDto>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments.FindAsync(request.Id);
        if(department is null)
            return Result.Failure<DepartmentDto>("Department not found");
        request.Adapt(department);
        var result = await _departmentRepository.Update(department, cancellationToken);
        return result.IsSuccess
            ? result.Value.Adapt<DepartmentDto>().AsSuccessResult()
            : Result.Failure<DepartmentDto>(result.Error);
    }
}
