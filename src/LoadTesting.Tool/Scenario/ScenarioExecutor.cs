using LoadTesting.Tool.Output;
using LoadTesting.Tool.Scenario.Runner;

namespace LoadTesting.Tool.Scenario;

internal class ScenarioExecutor : IScenarioExecutor
{
    private readonly IScenarioRunnerFactory _scenarioRunnerFactory;
    private readonly IOutputHelper _outputHelper;

    public ScenarioExecutor(IScenarioRunnerFactory scenarioRunnerFactory, IOutputHelper outputHelper)
    {
        _scenarioRunnerFactory = scenarioRunnerFactory;
        _outputHelper = outputHelper;
    }

    public async Task ExecuteAsync<TScenario>(int vus, int iterations, CancellationToken cancellationToken)
        where TScenario : IScenario
    {
        var runner = _scenarioRunnerFactory.CreateConcurrentUsersScenarioRunner(vus, iterations);

        var reporterCts = new CancellationTokenSource();
        using var reporterTask = Task.Factory.StartNew(async () =>
        {
            do
            {
                await _outputHelper.PrintProgressAsync(runner.ScenarioMeasurement);
                await Task.Delay(TimeSpan.FromSeconds(5), reporterCts.Token);
            } while (!reporterCts.IsCancellationRequested);
        }, reporterCts.Token);

        try
        {
            await runner.RunScenarioAsync<TScenario>(cancellationToken);
        }
        finally
        {
            reporterCts.Cancel();
        }


        await _outputHelper.PrintResultAsync(runner.ScenarioMeasurement);
    }
}