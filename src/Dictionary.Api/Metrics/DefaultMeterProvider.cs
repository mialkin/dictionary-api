using System.Diagnostics.Metrics;

namespace Dictionary.Api.Metrics;

public class DefaultMeterProvider : IDefaultMeterProvider
{
    public Meter Meter { get; }

    public DefaultMeterProvider(Meter meter) => Meter = meter;
}
