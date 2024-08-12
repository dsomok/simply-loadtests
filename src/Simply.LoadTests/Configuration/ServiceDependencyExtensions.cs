using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Simply.LoadTests.Measurement;
using Simply.LoadTests.Output;
using Simply.LoadTests.Scenario;

namespace Simply.LoadTests.Configuration;

public static class ServiceDependencyExtensions
{
    public static IServiceCollection AddSimplyLoadTests(this IServiceCollection services, params Assembly[] scenarioAssemblies)
    {
        services.AddSingleton<IScenarioMeasurer, ScenarioMeasurer>()
                .AddSingleton<IOutputHelper, ConsoleOutputHelper>();

        services.AddScenarioDependencies(scenarioAssemblies);

        return services;
    }
}
