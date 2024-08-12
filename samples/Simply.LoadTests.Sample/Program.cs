using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simply.LoadTests.Configuration;
using Simply.LoadTests.Sample;
using Simply.LoadTests.Scenario;

using var host = Host.CreateDefaultBuilder(args)
                     .ConfigureServices((ctx, services) =>
                     {
                         services.AddSimplyLoadTests(typeof(TestScenario).Assembly);
                     })
                     .Build();

Console.WriteLine("Press any key to start...");
Console.ReadKey();
Console.WriteLine("Running...");

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    Console.WriteLine("Cancelling...");
    cts.Cancel();
    e.Cancel = true;
};

await using (var scope = host.Services.CreateAsyncScope())
{
    var executor = scope.ServiceProvider.GetRequiredService<IScenarioExecutor>();
    await executor.ExecuteAsync<TestScenario>(20, 500, cts.Token);
}

Console.WriteLine("Stopping host...");
cts.Dispose();