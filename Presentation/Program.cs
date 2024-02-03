using System.Text.Json.Serialization;
using Application.Department.Dto;
using Domain.JWT;
using Domain.Role;
using Infrastructure.JWT;
using Infrastructure.Role;
using Microsoft.Extensions.Options;
using SchoolCleanArchitecture;
using SchoolCleanArchitecture.middleware;
using SchoolCleanArchitecture.Services;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters
        .Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExamplesFromAssemblyOf(typeof(DepartmentDto));
builder.Services.InstallServicesInAssembly(builder.Configuration);
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IJwtRepo, JwtRepo>();
builder.Services.AddTransient<IRoleRepo, RoleRepo>();

builder.Services.AddHttpContextAccessor();
/*
builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, _, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
*/
// SeriLog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Services.AddSerilog();
var app = builder.Build();
await app.MigrateAdminDb();
await app.MigrateTenantsDb();
//RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "School HR System v1");
    });
}



#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options!.Value);

#endregion

app.AddStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
/*app.UseSerilogRequestLogging();*/
app.MapControllers();
app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseMiddleware<TenantMiddleware>();
app.UseMiddleware<DbTransactionMiddleware>();
app.Run();