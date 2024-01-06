using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace SchoolCleanArchitecture.Services.Installer;

public class LocalizationInstaller:IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
       services.AddControllersWithViews();
       services.AddLocalization(opt =>
        {
            opt.ResourcesPath = "";
        });
        services.Configure<RequestLocalizationOptions>(options =>
        {
            List<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("ar-EG"),
                new CultureInfo("en-US"),
            };
            options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

    }
}