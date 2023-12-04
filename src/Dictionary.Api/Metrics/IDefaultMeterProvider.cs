using System.Diagnostics.Metrics;

namespace Dictionary.Api.Metrics;

public interface IDefaultMeterProvider
{
    public Meter Meter { get; }
}
