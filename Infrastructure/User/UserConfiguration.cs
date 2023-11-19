using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.User;

public class UserConfiguration:IEntityTypeConfiguration<Domain.Identity.User>
{
    public void Configure(EntityTypeBuilder<Domain.Identity.User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasValueGenerator<SeqIdValueGenerator>()
            .ValueGeneratedOnAdd();
    }
}