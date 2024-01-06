using Infrastructure.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Student;

public class StudentConfiguration:BaseConfiguration<Domain.Model.Student.Student>
{
    protected override void Configure(EntityTypeBuilder<Domain.Model.Student.Student> builder, string tableName)
    {
        builder.HasOne(x => x.User)
            .WithOne(c => c.Student)
            .HasForeignKey<Domain.Model.Student.Student>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}