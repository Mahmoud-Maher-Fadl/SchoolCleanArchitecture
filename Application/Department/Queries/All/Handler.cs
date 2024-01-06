using Application.Common;
using Application.Department.Dto;
using Application.Enums;
using Domain.common;
using Domain.Model.Department;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Department.Queries.All;

public class Handler:IRequestHandler<GetDepartmentsQuery,Result<PagingList<DepartmentDto>>>
{
    private readonly ApplicationDbContext _context;
    private readonly IDepartmentRepository _departmentRepository;

    public Handler(ApplicationDbContext context, IDepartmentRepository departmentRepository)
    {
        _context = context;
        _departmentRepository = departmentRepository;
    }


    public async Task<Result<PagingList<DepartmentDto>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        
        /*
        var departments = _context.Departments
            .Include(d => d.Students)
            .ThenInclude(s=>s.Department);
        var departmentsDto =await departments
            .AsQueryable()
            .ProjectToType<DepartmentDto>()
            .ToListAsync(cancellationToken);
        return departmentsDto.AsSuccessResult();
        */
        
        /*var departments =await _context.Departments
            .Include(d => d.Students)
            .ThenInclude(s=>s.Department)
            .AsQueryable()
            .ProjectToType<DepartmentDto>()
            .ToListAsync(cancellationToken);
        return departments.AsSuccessResult();*/
        
        // cause an error
        /*var departments =await _context.Departments
            .Include(d => d.Students)
            .ToListAsync(cancellationToken);
        var departmentsDto =await departments
            .AsQueryable()
            .ProjectToType<DepartmentDto>()
            .ToListAsync(cancellationToken);
        return departmentsDto.AsSuccessResult();*/
        DepartmentsOrderingEnum orderByEnum = request.OrderBy;
        var filter = orderByEnum switch
        {
           DepartmentsOrderingEnum.Name=>"Name",
           _ => "CreateDate"
        };
        var depts = await _departmentRepository.GetAllAsync(d => d.Users!, d => d.Subjects!);
        var departments =await _context.Departments
            .Where(s=>s.Name.Contains(request.Search))
            .OrderBy(( d) => EF.Property<object>(d, filter))
            .Skip((request.Page -1)*request.PageSize)
            .Take(request.PageSize)
            .ProjectToType<DepartmentDto>()
            .ToListAsync(cancellationToken);
        return new PagingList<DepartmentDto>(departments, request.Page, request.PageSize,request.Search,request.OrderBy).AsSuccessResult();
    }
}
