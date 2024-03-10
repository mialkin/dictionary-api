using System.Net;
using Dictionary.Api.Configurations;
using Dictionary.Api.Configurations.DataProtection;
using Dictionary.Api.Constants;
using Dictionary.Api.Endpoints;
using Dictionary.Api.Endpoints.Words;
using Dictionary.Api.Metrics;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

var services = builder.Services;

services.ConfigureDataProtection(builder.Configuration);
services.AddAuthentication(DefaultAuthenticationScheme.Name)
    .AddCookie(DefaultAuthenticationScheme.Name, options =>
    {
        options.Cookie.Name = "Dictionary.Session";
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        };
    });

services.AddAuthorization();

services.AddControllers();
services.AddRouting(options => options.LowercaseUrls = true);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.DescribeAllParametersInCamelCase();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Dictionary API", Version = "v1" });
});

services.ConfigureApplication(builder.Configuration);
services.ConfigureMetrics();

var application = builder.Build();

application.UseRouting();
application.UseAuthentication();
application.UseAuthorization();

application.UseSerilogRequestLogging();

application.UseSwagger();
application.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "Dictionary API";
});

application.MapApplicationEndpoints();

application
    .UseOpenTelemetryPrometheusScrapingEndpoint(); // Requires OpenTelemetry.Exporter.Prometheus.AspNetCore package

application.Run();

// TODO Replace Makefile with bash.sh

// Use Dependabot for updating dependencies https://github.com/serilog-contrib/serilog-sinks-grafana-loki/commits?author=dependabot%5Bbot%5D

public partial class Program
{
} // Public modifier is needed for WebApplicationFactory<Program> instance creation in integration tests
