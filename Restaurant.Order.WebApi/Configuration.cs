using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Order.Application.Interfaces.WebApi;
using Restaurant.Order.WebApi.Security;
using Restaurant.Order.WebApi.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Restaurant.Order.WebApi;

public static class Configuration
{

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ISecurityService, SecurityService>();
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
            });

        return services;
    }

    public static IServiceCollection AddRestaurantAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(config =>
        {
            foreach (var claim in Claims.All)
                config.AddPolicy(claim, builder => builder.RequireClaim(ClaimTypes.Role, claim));
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
