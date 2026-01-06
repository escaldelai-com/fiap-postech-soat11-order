using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Restaurant.Order.WebApi;

public static class Configuration
{

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = config["Security:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = config["Security:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = GetKey(config),
                    ValidateLifetime = true,
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtAuth");
                        logger.LogError(context.Exception, "JWT authentication failed");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtAuth");
                        logger.LogWarning("OnChallenge: error={Error}, description={Description}", context.Error, context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtAuth");
                        logger.LogInformation("Token validated for {sub}", context.Principal?.FindFirst("sub")?.Value);
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

    public static IServiceCollection AddRestaurantAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(config =>
        {
            config.AddPolicy("client", builder => builder.RequireClaim(ClaimTypes.Role, "client"));
            config.AddPolicy("payment", builder => builder.RequireClaim(ClaimTypes.Role, "payment"));
            config.AddPolicy("admin", builder => builder.RequireClaim(ClaimTypes.Role, "admin"));
        });

        return services;
    }



    private static RsaSecurityKey GetKey(IConfiguration config)
    {
        var rsa = RSA.Create();
        var key = Convert.FromBase64String(config["Security:PublicKey"]
            ?? throw new ArgumentNullException("Security:PublicKey"));

        rsa.ImportRSAPublicKey(key, out _);

        return new RsaSecurityKey(rsa);
    }

}
