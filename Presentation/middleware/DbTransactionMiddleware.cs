using Domain.common;
using Microsoft.EntityFrameworkCore;

namespace SchoolCleanArchitecture.middleware;

public class DbTransactionMiddleware
{
    private readonly RequestDelegate _next;

    public DbTransactionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IApplicationDbContext context, IAdminContext adminContext)
    {
        var transaction = await context.BeginTransactionAsync();
        var adminTransaction = await adminContext.BeginTransactionAsync();
        try
        { 
            await _next(httpContext);
            if (httpContext.Response.StatusCode == 400)
            {
                await transaction.RollbackAsync();
                await adminTransaction.RollbackAsync();
                return;
            }
            await context.SaveChangesAsync();
            await adminContext.SaveChangesAsync();

            await transaction.CommitAsync();
            await adminTransaction.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            await adminTransaction.RollbackAsync();
            throw;
        }
    }
}