using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace FairRent.Api.Auth;

public static class AuthExtensions
{
    public static IServiceCollection AddAuth0Jwt(this IServiceCollection services, IConfiguration config)
    {
        var domain = config["Auth0:Domain"];
        var audience = config["Auth0:Audience"];

        if (string.IsNullOrWhiteSpace(domain) || string.IsNullOrWhiteSpace(audience))
            throw new InvalidOperationException("Auth0 configuration is missing (Domain or Audience).");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.Authority = $"https://{domain}/";
                o.Audience = audience;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });

        services.AddSingleton<IAuthorizationHandler, ScopeRequirementHandler>();

        services.AddAuthorization();

        return services;
    }
}
