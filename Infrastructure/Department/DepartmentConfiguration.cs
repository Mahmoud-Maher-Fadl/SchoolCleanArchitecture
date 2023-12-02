using Infrastructure.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Department;

public class DepartmentConfiguration:BaseConfiguration<Domain.Model.Department.Department>
{
    protected override void Configure(EntityTypeBuilder<Domain.Model.Department.Department> builder, string tableName)
    {
        
    }
}