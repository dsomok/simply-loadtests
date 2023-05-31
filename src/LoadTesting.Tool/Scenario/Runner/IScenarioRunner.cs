using LoadTesting.Tool.Measurement.Models;

namespace LoadTesting.Tool.Scenario.Runner;

public interface IScenarioRunner
{
    ScenarioMeasurement ScenarioMeasurement { get; }

    Task<ScenarioMeasurement> RunScenarioAsync<TScenario>(CancellationToken cancellationToken)
        where TScenario : IScenario;
}