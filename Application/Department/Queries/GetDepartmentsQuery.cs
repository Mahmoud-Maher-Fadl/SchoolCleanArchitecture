using Application.Department.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Department.Queries;

public class GetDepartmentsQuery:IRequest<Result<List<DepartmentDto>>>
{
    public class Handler:IRequestHandler<GetDepartmentsQuery,Result<List<DepartmentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Result<List<DepartmentDto>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
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
            
            var departments =await _context.Departments
                /*.Include(d => d.Students)
                .ThenInclude(s=>s.Department) 
                */
                .ProjectToType<DepartmentDto>()
                .ToListAsync(cancellationToken);
            return departments.AsSuccessResult();
        }
    }
}