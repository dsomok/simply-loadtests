using LoadTesting.Tool.Measurement;
using LoadTesting.Tool.Measurement.Models;

namespace LoadTesting.Tool.Scenario.Runner;

internal abstract class BaseScenarioRunner : IScenarioRunner
{
    private readonly IScenarioFactory _scenarioFactory;

    protected BaseScenarioRunner(int vusCount, IScenarioMeasurer scenarioMeasurer, IScenarioFactory scenarioFactory)
    {
        _scenarioFactory = scenarioFactory;

        VusCount = vusCount;
        ScenarioMeasurement = scenarioMeasurer.CreateMeasurement(vusCount);
    }


    public ScenarioMeasurement ScenarioMeasurement { get; }

    protected int VusCount { get; }


    public Task<ScenarioMeasurement> RunScenarioAsync<TScenario>(CancellationToken cancellationToken)
        where TScenario : IScenario
    {
        var scenario = _scenarioFactory.CreateScenario<TScenario>();
        return RunScenarioAsync(scenario, cancellationToken);
    }

    protected abstract Task<ScenarioMeasurement> RunScenarioAsync(IScenario scenario, CancellationToken cancellationToken);
}