using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SchoolCleanArchitecture.middleware;

public class DbTransactionMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ApplicationDbContext _context;

    public DbTransactionMiddleware(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.GetType().Name.Contains("Query"))
            return await next();
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var response = await next();
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return response;
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}