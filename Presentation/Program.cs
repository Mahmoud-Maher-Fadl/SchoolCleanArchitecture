using System.Globalization;
using System.Text.Json.Serialization;
using Domain.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using SchoolCleanArchitecture;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using SchoolCleanArchitecture.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Localization Service

builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
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

#endregion

builder.Services
    .AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InstallServicesInAssembly(builder.Configuration);

/*builder.Services.AddIdentity<SchoolUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();*/


builder.Services.AddHttpContextAccessor();
builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, _, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
var app = builder.Build();
await app.AddSeeds();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Localization Middleware

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options!.Value);


#endregion


app.UseAuthorization();
app.UseAuthentication();
app.UseSerilogRequestLogging();
app.MapControllers();
app.Run();