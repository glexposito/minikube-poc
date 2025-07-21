Console.WriteLine("Starting high-load test...");

const int workerCount = 500; // Number of concurrent workers
const string url = "http://127.0.0.1:35165/node-id";

var client = new HttpClient(new SocketsHttpHandler
{
    MaxConnectionsPerServer = workerCount * 2 // Allow burst
});

var tasks = new List<Task>();

for (var i = 0; i < workerCount; i++)
{
    var id = i;
    tasks.Add(Task.Run(async () =>
    {
        while (true)
        {
            try
            {
                var response = await client.GetAsync(url);
                _ = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[{id}] {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{id}] Error: {ex.Message}");
            }
        }
    }));
}

await Task.WhenAll(tasks); // Runs forever