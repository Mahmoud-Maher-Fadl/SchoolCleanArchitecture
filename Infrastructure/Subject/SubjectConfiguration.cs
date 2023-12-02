using Infrastructure.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Subject;

public class SubjectConfiguration:BaseConfiguration<Domain.Model.Subject.Subject>
{
    protected override void Configure(EntityTypeBuilder<Domain.Model.Subject.Subject> builder, string tableName)
    {
        builder.HasOne(s => s.Department)
            .WithMany(d => d.Subjects)
            .HasForeignKey(s=>s.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull); 
     
    }
}