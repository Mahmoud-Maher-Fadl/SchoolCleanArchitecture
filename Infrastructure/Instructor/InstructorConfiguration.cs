using Infrastructure.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Instructor;

public class InstructorConfiguration:BaseConfiguration<Domain.Model.Instructor.Instructor>
{
    protected override void Configure(EntityTypeBuilder<Domain.Model.Instructor.Instructor> builder, string tableName)
    {
        builder.HasOne(x => x.User)
            .WithOne(c => c.Instructor)
            .HasForeignKey<Domain.Model.Instructor.Instructor>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}