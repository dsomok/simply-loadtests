using Microsoft.Extensions.DependencyInjection;
using Simply.LoadTests.Scenario.Runner.Concrete;

namespace Simply.LoadTests.Scenario.Runner;

internal class ScenarioRunnerFactory : IScenarioRunnerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ScenarioRunnerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IScenarioRunner CreateConcurrentUsersScenarioRunner(int concurrentUsersCount, int iterationsCount)
    {
        return ActivatorUtilities.CreateInstance<ConcurrentUsersScenarioRunner>(_serviceProvider, concurrentUsersCount, iterationsCount);
    }
}