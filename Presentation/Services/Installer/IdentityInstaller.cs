using Domain.JWT;
using Domain.Role;
using Domain.Tenant;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using School;

namespace SchoolCleanArchitecture.Services.Installer;

public class IdentityInstaller:IServiceInstaller
{
  public void InstallServices(IServiceCollection service, IConfiguration configuration)
  {
      service.AddIdentityCore<Domain.Tenant.Tenant>(option =>
          {
              option.SignIn.RequireConfirmedEmail = false;
              option.User.RequireUniqueEmail = false;
              option.Password.RequireDigit = false;
              option.Password.RequireLowercase = false;
              option.Password.RequireUppercase = false;
              option.Password.RequiredLength = 5;
              option.Lockout.MaxFailedAccessAttempts = 3;
          })
          .AddRoles<Role>()
          .AddRoleManager<RoleManager<Role>>()
          .AddEntityFrameworkStores<AdminContext>();
  }
}