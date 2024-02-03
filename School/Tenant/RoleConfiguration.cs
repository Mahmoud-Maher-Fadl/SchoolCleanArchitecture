using Domain.Role;
using Infrastructure.common.DatabaseGenerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace School.Tenant;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<SeqIdValueGenerator>();

        builder.HasOne(x => x.School)
            .WithMany(x => x.Roles)
            .HasForeignKey(x => x.SchoolId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}