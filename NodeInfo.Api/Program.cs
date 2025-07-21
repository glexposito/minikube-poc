using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using NodeInfo.Api;

var builder = WebApplication.CreateBuilder(args);

// Register health checks
builder.Services.AddHealthChecks()
    .AddCheck<LivenessCheck>("self") // Optional, not used due to Predicate = _ => false
    .AddCheck<ReadinessCheck>("readiness", tags: ["ready"]);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/node-id", () =>
{
    var nodeId = Environment.MachineName;
    return Results.Ok(new { NodeId = nodeId });
});

app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = _ => false
});

app.MapHealthChecks("/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.Run();

public abstract partial class Program;