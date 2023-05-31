using Microsoft.Extensions.DependencyInjection;

namespace LoadTesting.Tool.Scenario;

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