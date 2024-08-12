using Simply.LoadTests.Scenario;

namespace Simply.LoadTests.Sample;

public class TestScenario : IScenario
{
    public async Task ExecuteAsync(ScenarioContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
    }
}