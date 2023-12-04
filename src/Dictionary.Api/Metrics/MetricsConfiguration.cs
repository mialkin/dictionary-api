using System.Reflection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace Dictionary.Api.Metrics;

public static class MetricsConfiguration
{
    public static void ConfigureMetrics(this IServiceCollection services)
    {
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        var resourceBuilder = ResourceBuilder.CreateDefault().AddService(serviceName: assemblyName!);
        const string defaultMeterName = nameof(DefaultMeterProvider);

        services.AddOpenTelemetry() // Requires OpenTelemetry.Extensions.Hosting
            .WithMetrics(x =>
            {
                x.SetResourceBuilder(resourceBuilder);
                x.AddRuntimeInstrumentation(); // Requires OpenTelemetry.Instrumentation.Runtime
                x.AddAspNetCoreInstrumentation(); // Requires OpenTelemetry.Instrumentation.AspNetCore --prerelease
                x.AddHttpClientInstrumentation(); // Requires OpenTelemetry.Instrumentation.Http
                x.AddPrometheusExporter(); // Requires OpenTelemetry.Exporter.Prometheus.AspNetCore
                x.AddMeter(defaultMeterName);
            });

    }
}
