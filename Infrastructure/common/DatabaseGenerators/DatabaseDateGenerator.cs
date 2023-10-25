﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure;

public class DatabaseDateGenerator : ValueGenerator<DateTime>
{
    public override DateTime Next(EntityEntry entry) => DateTime.Now;
    public override bool GeneratesTemporaryValues => false;
}