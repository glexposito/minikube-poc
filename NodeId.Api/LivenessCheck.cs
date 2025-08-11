using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NodeId.Api;

public class LivenessCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("App is alive"));
    }
}
