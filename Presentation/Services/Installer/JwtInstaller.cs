using System.Text;
using Domain.JWT;
using Infrastructure.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace SchoolCleanArchitecture.Services.Installer;

public class JwtInstaller:IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = new JwtOptions();
        configuration.GetSection(nameof(jwtOptions)).Bind(jwtOptions);
        services.AddSingleton(jwtOptions);
        services.AddTransient<IJwtService, JwtService>();

        
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = jwtOptions.ValidateIssuer,
                        ValidIssuers = new []{jwtOptions.Issuer},
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        ValidateIssuerSigningKey = jwtOptions.ValidateIssuerSigningKey,
                        ValidAudience = jwtOptions.Audience,
                        ValidateAudience = jwtOptions.ValidateAudience,
                        ValidateLifetime = jwtOptions.ValidateLifeTime
                    };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });
    }
}



