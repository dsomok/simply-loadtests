namespace Simply.LoadTests.Scenario;

public interface IScenario
{
    Task BeforeScenarioAsync(ScenarioContext context, CancellationToken cancellationToken) => Task.CompletedTask;

    Task ExecuteAsync(ScenarioContext context, CancellationToken cancellationToken);

    Task AfterScenarioAsync(ScenarioContext context, CancellationToken cancellationToken) => Task.CompletedTask;
}