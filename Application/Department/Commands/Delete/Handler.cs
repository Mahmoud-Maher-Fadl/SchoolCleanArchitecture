﻿using Application.Department.Dto;
using Application.Localization;
using Domain.common;
using Domain.Model.Department;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Department.Commands.Delete;

public class Handler:IRequestHandler<DeleteDepartmentCommand,Result<DepartmentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;

    public Handler(IApplicationDbContext context, IDepartmentRepository departmentRepository, IStringLocalizer<SharedResources> stringLocalizer)
    {
        _context = context;
        _departmentRepository = departmentRepository;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<Result<DepartmentDto>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments.FindAsync(request.Id);
        if(department is null)
            return Result.Failure<DepartmentDto>(_stringLocalizer[SharedResourcesKeys.NotFound]);
        var result = await _departmentRepository.Delete(department, cancellationToken);
        return result.IsSuccess
            ? result.Value.Adapt<DepartmentDto>().AsSuccessResult()
            : Result.Failure<DepartmentDto>(result.Error);
    }
}
