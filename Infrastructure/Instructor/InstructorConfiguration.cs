using Infrastructure.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Instructor;

public class InstructorConfiguration:BaseConfiguration<Domain.Model.Instructor.Instructor>
{
    protected override void Configure(EntityTypeBuilder<Domain.Model.Instructor.Instructor> builder, string tableName)
    {
        builder.HasOne(i => i.Department)
            .WithMany(d => d.Instructors)
            .HasForeignKey(i => i.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);
        
           
        builder.HasMany(i => i.Subjects)
            .WithOne(s => s.Instructor)
            .HasForeignKey(s=>s.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}