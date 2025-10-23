using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace FairRent.Api.Auth;

public static class AuthorizationPolicies
{
    public const string UsersRead = "users.read";
    public const string UsersWrite = "users.write";

    public static IServiceCollection AddScopePolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(UsersRead, p => p.Requirements.Add(new ScopeRequirement("read:users")));
            options.AddPolicy(UsersWrite, p => p.Requirements.Add(new ScopeRequirement("write:users")));
        });
        return services;
    }
}
