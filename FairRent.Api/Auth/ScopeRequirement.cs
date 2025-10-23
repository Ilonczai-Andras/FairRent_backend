using Microsoft.AspNetCore.Authorization;

namespace FairRent.Api.Auth;
public sealed class ScopeRequirement : IAuthorizationRequirement
{
    public string Scope { get; }

    public ScopeRequirement(string scope)
    {
        Scope = scope;
    }
}
