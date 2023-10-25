using Infrastructure.common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Subject;

public class SubjectConfiguration:BaseConfiguration<Domain.Model.Subject.Subject>
{
    protected override void Configure(EntityTypeBuilder<Domain.Model.Subject.Subject> builder, string tableName)
    {
        
    }
}