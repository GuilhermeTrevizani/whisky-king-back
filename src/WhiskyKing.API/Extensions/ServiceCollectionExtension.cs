using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WhiskyKing.API.Authorization;
using WhiskyKing.Core.Models.Settings;
using WhiskyKing.Domain.Enums;
using WhiskyKing.Infra.Data;

namespace WhiskyKing.API.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddDatabase(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<DatabaseContext>(x =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            x.LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
    }

    public static void AddSettings(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
    }

    public static void AddAuthenticationJwt(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var jwtKey = builder.Configuration.GetSection("Jwt").GetValue<string>("Key") ?? string.Empty;

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
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }

    public static void AddPolicy(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Default", policy => policy.Requirements.Add(new AuthorizationRequirement([])));
            options.DefaultPolicy = options.GetPolicy("Default")!;
            foreach (var permission in Enum.GetValues<Permission>())
                options.AddPolicy(permission.ToString(),
                    policy => policy.Requirements.Add(new AuthorizationRequirement([permission])));
        });
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Whisky King", Version = "v1" });
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            x.CustomSchemaIds(type => type.ToString().Replace('+', '.').Replace($"{type.Namespace}.", string.Empty));
        });
    }
}