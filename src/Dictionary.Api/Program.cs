using Dictionary.Api.Configurations;
using Dictionary.Api.Metrics;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

var services = builder.Services;

services.AddControllers();
services.AddRouting(options => options.LowercaseUrls = true);
services.AddSwaggerGen(options => { options.DescribeAllParametersInCamelCase(); });
services.ConfigureApplication(builder.Configuration);
services.ConfigureMetrics();

var application = builder.Build();

application.UseSerilogRequestLogging();

application.UseSwagger();
application.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
    options.RoutePrefix = string.Empty;
});

application.MapControllers();
application.UseRouting();
application
    .UseOpenTelemetryPrometheusScrapingEndpoint(); // Requires OpenTelemetry.Exporter.Prometheus.AspNetCore package

application.Run();

// TODO Replace Makefile with bash.sh

// Use Dependabot for updating dependencies https://github.com/serilog-contrib/serilog-sinks-grafana-loki/commits?author=dependabot%5Bbot%5D

public partial class Program
{
} // Public modifier is needed for WebApplicationFactory<Program> instance creation in integration tests
