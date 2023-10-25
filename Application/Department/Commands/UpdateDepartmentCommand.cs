using Application.Department.Dto;
using Domain.common;
using Domain.Model.Department;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Department.Commands;

public class UpdateDepartmentCommand:IRequest<Result<DepartmentDto>>
{
    public string Id { get; set; }
    public string Name { get; set; } 
    public string Location { get; set; } 
    
    public class Validator:AbstractValidator<UpdateDepartmentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Location).NotEmpty();
        }
        
    }

    public class Handler:IRequestHandler<UpdateDepartmentCommand,Result<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ApplicationDbContext _context;

        public Handler(IDepartmentRepository departmentRepository, ApplicationDbContext context)
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
}