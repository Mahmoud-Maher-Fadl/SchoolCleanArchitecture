using Infrastructure.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Department;

public class DepartmentConfiguration:BaseConfiguration<Domain.Model.Department.Department>
{
    protected override void Configure(EntityTypeBuilder<Domain.Model.Department.Department> builder, string tableName)
    {
        builder.HasMany(d => d.Students)
            .WithOne(s => s.Department)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(department => department.Subjects)
            .WithOne(subject => subject.Department)
            .HasForeignKey(s=>s.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}