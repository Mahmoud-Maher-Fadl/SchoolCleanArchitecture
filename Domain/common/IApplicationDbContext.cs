using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.common;

public interface IApplicationDbContext
{
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}