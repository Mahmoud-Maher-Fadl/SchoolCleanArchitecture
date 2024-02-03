using Infrastructure.common.DatabaseGenerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace School.Tenant;

public class TenantConfiguration : IEntityTypeConfiguration<Domain.Tenant.Tenant>
{
    public void Configure(EntityTypeBuilder<Domain.Tenant.Tenant> builder)
    {
        builder.ToTable("Tenants");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<SeqIdValueGenerator>();


        builder.HasOne(x => x.School)
            .WithMany(x => x.Tenants)
            .HasForeignKey(x => x.SchoolId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(x=>x.Email).IsUnique();
    }
}