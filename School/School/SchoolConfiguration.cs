using Infrastructure.common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace School.School;

public class SchoolConfiguration:BaseConfiguration<Domain.Tenant.School>
{
    protected override void Configure(EntityTypeBuilder<Domain.Tenant.School> builder, string tableName)
    {
        
    }
}