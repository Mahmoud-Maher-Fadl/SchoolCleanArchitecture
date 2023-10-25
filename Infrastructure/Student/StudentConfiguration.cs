using Infrastructure.common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Student;

public class StudentConfiguration:BaseConfiguration<Domain.Model.Student.Student>
{
    protected override void Configure(EntityTypeBuilder<Domain.Model.Student.Student> builder, string tableName)
    {
        
    }
}