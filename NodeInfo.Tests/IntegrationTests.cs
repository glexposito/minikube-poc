using System.Net;
using Shouldly;

namespace NodeInfo.Tests;

[Collection("WebApp Collection")]
public class IntegrationTests(WebApplicationFactoryFixture fixture)
{
    private readonly HttpClient _client = fixture.Client;

    [Fact]
    public async Task GetNodeId_Returns200Ok()
    {
        var response = await _client.GetAsync("/node-id", TestContext.Current.CancellationToken);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task LivenessEndpoint_Returns200Ok()
    {
        var response = await _client.GetAsync("/healthz", TestContext.Current.CancellationToken);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ReadinessEndpoint_Returns200Ok()
    {
        var response = await _client.GetAsync("/ready", TestContext.Current.CancellationToken);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}