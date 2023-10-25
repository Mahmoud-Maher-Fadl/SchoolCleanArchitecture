using System.Text.Json.Serialization;
using Erp;
using Erp.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InstallServicesInAssembly(builder.Configuration);
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


app.UseAuthorization();
app.UseAuthentication();
app.UseSerilogRequestLogging();
app.MapControllers();
app.Run();