using Application.Department.Dto;
using Domain.common;
using Domain.Model.Department;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Department.Commands;

public class DeleteDepartmentCommand:IRequest<Result<DepartmentDto>>
{
    public string Id { get; set; }
    public class Handler:IRequestHandler<DeleteDepartmentCommand,Result<DepartmentDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;

        public Handler(ApplicationDbContext context, IDepartmentRepository departmentRepository)
        {
            _context = context;
            _departmentRepository = departmentRepository;
        }

        public async Task<Result<DepartmentDto>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments.FindAsync(request.Id);
            if(department is null)
                return Result.Failure<DepartmentDto>("Department Not Found");
            var result = await _departmentRepository.Delete(department, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<DepartmentDto>().AsSuccessResult()
                : Result.Failure<DepartmentDto>(result.Error);
        }
    }
}