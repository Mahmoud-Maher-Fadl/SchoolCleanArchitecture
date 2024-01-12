using System.Text.Json.Serialization;
using Application.Department.Dto;
using Domain.JWT;
using Domain.Role;
using Infrastructure.JWT;
using Infrastructure.Role;
using Microsoft.Extensions.Options;
using SchoolCleanArchitecture;
using SchoolCleanArchitecture.Services;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerExamplesFromAssemblyOf(typeof(DepartmentDto));
builder.Services.InstallServicesInAssembly(builder.Configuration);


builder.Services.AddTransient<IJwtRepo, JwtRepo>();
builder.Services.AddTransient<IRoleRepo, RoleRepo>();

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


app.UseAuthentication();
app.UseAuthorization();
app.UseSerilogRequestLogging();
app.MapControllers();
app.Run();