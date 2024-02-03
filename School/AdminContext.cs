using Domain.common;
using Domain.Role;
using Domain.Tenant;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace School;

public class AdminContext:IdentityDbContext<Domain.Tenant.Tenant>,IAdminContext
{
    public AdminContext(DbContextOptions<AdminContext> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AdminContext).Assembly);
    }

    public DbSet<Domain.Tenant.School> Schools { get; set; }
    public DbSet<Domain.Tenant.Tenant> Tenants { get; set; }
    public DbSet<Role> Roles { get; set; }
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }
}