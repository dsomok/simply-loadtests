using Simply.LoadTests.Measurement.Models;

namespace Simply.LoadTests.Scenario.Runner;

public interface IScenarioRunner
{
    ScenarioMeasurement ScenarioMeasurement { get; }

    Task<ScenarioMeasurement> RunScenarioAsync<TScenario>(CancellationToken cancellationToken)
        where TScenario : IScenario;
}