namespace Simply.LoadTests.Scenario;

public interface IScenarioExecutor
{
    Task ExecuteAsync<TScenario>(int vus, int iterations, CancellationToken cancellationToken)
        where TScenario : IScenario;
}
