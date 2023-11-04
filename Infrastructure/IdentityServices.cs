using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure;

public static class IdentityServices
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<Domain.Identity.User,IdentityRole>(option =>
        {
            option.SignIn.RequireConfirmedEmail = false;
            option.User.RequireUniqueEmail = true;
            option.Password.RequireDigit = true;
            option.Password.RequireLowercase = true;
            option.Password.RequireUppercase = true;
            option.Password.RequiredLength = 10;
            option.Lockout.MaxFailedAccessAttempts = 3;
        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        return serviceCollection;
    }
}