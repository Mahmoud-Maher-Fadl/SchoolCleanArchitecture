using Application.Common;
using Application.Department.Dto;
using Application.Enums;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Department.Queries;

public class GetDepartmentsQuery:IRequest<Result<PagingList<DepartmentDto>>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }  
    public string Search { get; set; }=String.Empty;

    public DepartmentsOrderingEnum OrderBy { get; set; }

    public class Handler:IRequestHandler<GetDepartmentsQuery,Result<PagingList<DepartmentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
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
            var departments =await _context.Departments
                .Where(s=>s.Name.Contains(request.Search))
                .OrderBy(( d) => EF.Property<object>(d, filter))
                .Skip((request.Page -1)*request.PageSize)
                .Take(request.PageSize)
                /*.Include(d => d.Students)
                .ThenInclude(s=>s.Department) 
                */
                .ProjectToType<DepartmentDto>()
                .ToListAsync(cancellationToken);
            return new PagingList<DepartmentDto>(departments, request.Page, request.PageSize,request.Search,request.OrderBy).AsSuccessResult();
        }
    }
}