using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NodeInfo.Api;

public class ReadinessCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        // Simulate readiness (e.g., DB reachable)
        var isReady = true;

        return Task.FromResult(
            isReady
                ? HealthCheckResult.Healthy("Ready")
                : HealthCheckResult.Unhealthy("Not ready"));
    }
}
