using Microsoft.Extensions.DependencyInjection;

namespace Simply.LoadTests.Scenario;

internal class ScenarioFactory : IScenarioFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ScenarioFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IScenario CreateScenario<TScenario>()
        where TScenario : IScenario
    {
        return _serviceProvider.GetRequiredService<TScenario>();
    }
}