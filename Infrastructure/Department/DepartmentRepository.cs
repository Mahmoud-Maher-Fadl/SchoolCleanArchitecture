using Domain.Model.Department;
using Infrastructure.common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Department;

public class DepartmentRepository:BaseSqlRepositoryImpl<Domain.Model.Department.Department>,IDepartmentRepository
{
    public DepartmentRepository(ApplicationDbContext context) : base(context.Departments)
    {
    }
}