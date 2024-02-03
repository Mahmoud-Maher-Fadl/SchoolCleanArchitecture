using System.Security.Claims;
using Domain.common;
using Infrastructure.common;
using Microsoft.EntityFrameworkCore;

namespace SchoolCleanArchitecture.middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IAdminContext adminContext, IConfiguration configuration)
    {
        var tenantEmail = httpContext.User.FindFirstValue(ClaimTypes.Email);
        if (tenantEmail == null)
        {
            await _next(httpContext);
            return;
        }

        var tenant = await adminContext.Tenants.FirstOrDefaultAsync(x => x.Email == tenantEmail);
        if (tenant == null)
        {
            await _next(httpContext);
            return;
        }

        var connectionString = configuration.GetNewConnectionString(tenant.SchoolId);
        TenantContainer.ConnectionString = connectionString;
        await _next(httpContext);
    }
}

