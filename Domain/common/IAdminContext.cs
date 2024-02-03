using Domain.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.common;

public interface IAdminContext
{
    public DbSet<School>Schools { get; set; }
    public DbSet<Tenant.Tenant>Tenants { get; set; }
    public DbSet<Role.Role>Roles { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

}