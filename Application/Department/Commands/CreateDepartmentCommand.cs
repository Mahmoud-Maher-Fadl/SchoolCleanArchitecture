using Application.Department.Dto;
using Domain.common;
using Domain.Model.Department;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Department.Commands;

public class CreateDepartmentCommand:IRequest<Result<DepartmentDto>>
{
    public string Name { get; set; } 
    public string Location { get; set; } 
    public class Validator:AbstractValidator<CreateDepartmentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Location).NotEmpty();
        }
    }
    public class Handler:IRequestHandler<CreateDepartmentCommand,Result<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public Handler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Result<DepartmentDto>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = request.Adapt<Domain.Model.Department.Department>();
            var result = await _departmentRepository.Add(department, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<DepartmentDto>().AsSuccessResult()
                : Result.Failure<DepartmentDto>(result.Error);
        }
    }
}