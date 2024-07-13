using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Api.IntegrationTests.Words;

public class WordEndpointsWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? HostEnvironment.Ide;
        builder.UseEnvironment(environment);

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
        });
    }
}
