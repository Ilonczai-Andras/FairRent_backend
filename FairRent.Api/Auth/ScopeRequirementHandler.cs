using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FairRent.Api.Auth;

public sealed class ScopeRequirementHandler : AuthorizationHandler<ScopeRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ScopeRequirement requirement)
    {
        var permissions = context.User.FindAll("permissions").Select(c => c.Value);
        if (permissions.Contains(requirement.Scope, StringComparer.Ordinal))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var scopeClaim = context.User.FindFirst("scope")?.Value;
        if (!string.IsNullOrWhiteSpace(scopeClaim))
        {
            var scopes = scopeClaim.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (scopes.Contains(requirement.Scope, StringComparer.Ordinal))
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}
