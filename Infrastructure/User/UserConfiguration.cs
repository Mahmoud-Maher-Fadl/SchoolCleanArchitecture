using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.User;

public class UserConfiguration:IEntityTypeConfiguration<Domain.Identity.SchoolUser>
{
    public void Configure(EntityTypeBuilder<Domain.Identity.SchoolUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasValueGenerator<SeqIdValueGenerator>()
            .ValueGeneratedOnAdd();
    }
}