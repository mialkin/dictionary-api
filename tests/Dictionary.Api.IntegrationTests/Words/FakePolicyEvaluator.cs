using System.Security.Claims;
using Dictionary.Api.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Dictionary.Api.IntegrationTests.Words;

public sealed class FakePolicyEvaluator : IPolicyEvaluator
{
    public async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var principal = new ClaimsPrincipal();

        principal.AddIdentity(
            new ClaimsIdentity(
                claims: new[] { new Claim(type: ClaimTypes.Name, value: Guid.NewGuid().ToString()) },
                authenticationType: DefaultAuthenticationScheme.Name));

        return await Task.FromResult(AuthenticateResult.Success(
            new AuthenticationTicket(
                principal,
                properties: new AuthenticationProperties(),
                authenticationScheme: DefaultAuthenticationScheme.Name)));
    }

    public async Task<PolicyAuthorizationResult> AuthorizeAsync(
        AuthorizationPolicy policy,
        AuthenticateResult authenticationResult,
        HttpContext context,
        object? resource)
    {
        return await Task.FromResult(PolicyAuthorizationResult.Success());
    }
}
