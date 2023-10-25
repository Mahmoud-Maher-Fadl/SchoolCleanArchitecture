using Domain.common;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.common;

public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasValueGenerator<SeqIdValueGenerator>()
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.CreateDate)
            .HasValueGenerator<DatabaseDateGenerator>()
            .ValueGeneratedOnAdd();
        builder.Property(x => x.UpdateDate)
            .HasValueGenerator<DatabaseDateGenerator>()
            .ValueGeneratedOnUpdate();
        
            Configure(builder, typeof(T).Name);
    }

    protected abstract void Configure(EntityTypeBuilder<T> builder, string tableName);
}