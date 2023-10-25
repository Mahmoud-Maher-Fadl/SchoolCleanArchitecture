using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure;

public class SeqIdValueGenerator : ValueGenerator<string>
{
    public override string Next(EntityEntry entry)
    {
        return Guid.NewGuid().ToString().ToLower();
    }

    public override bool GeneratesTemporaryValues => false;
}