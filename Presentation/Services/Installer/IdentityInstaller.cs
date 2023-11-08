using Infrastructure;
using Microsoft.AspNetCore.Identity;
namespace SchoolCleanArchitecture.Services.Installer;

public class IdentityInstaller:IServiceInstaller
{
  public void InstallServices(IServiceCollection service, IConfiguration configuration)
  {
      service.AddIdentity<Domain.Identity.SchoolUser,IdentityRole>(option =>
      {
          option.SignIn.RequireConfirmedEmail = false;
          option.User.RequireUniqueEmail = false;
          option.Password.RequireDigit = false;
          option.Password.RequireLowercase = false;
          option.Password.RequireUppercase = false;
          option.Password.RequiredLength = 5;
          option.Lockout.MaxFailedAccessAttempts = 3;
      }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
  }
}