using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Dictionary.Api.IntegrationTests.Words.Infrastructure;

public class WordsControllerWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // TODO Search for ConfigureWebHost in GitLab
    }
}
