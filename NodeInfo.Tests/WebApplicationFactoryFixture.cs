using Microsoft.AspNetCore.Mvc.Testing;


namespace NodeInfo.Tests;

// ReSharper disable once ClassNeverInstantiated.Global
public class WebApplicationFactoryFixture : IAsyncLifetime
{
    private WebApplicationFactory<Program> Factory { get; }
    public HttpClient Client { get; }
    
    public WebApplicationFactoryFixture()
    {
        Factory = new WebApplicationFactory<Program>();
        Client = Factory.CreateClient();
    }

    public async ValueTask InitializeAsync() => await Task.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        Client.Dispose();
        await Factory.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}