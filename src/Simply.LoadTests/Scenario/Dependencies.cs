using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Simply.LoadTests.Scenario.Runner;

namespace Simply.LoadTests.Scenario;

public static class Dependencies
{
    internal static IServiceCollection AddScenarioDependencies(this IServiceCollection services, params Assembly[] scenarioAssemblies)
    {
        return services.AddSingleton<IScenarioFactory, ScenarioFactory>()
                       .AddSingleton<IScenarioRunnerFactory, ScenarioRunnerFactory>()
                       .AddTransient<IScenarioExecutor, ScenarioExecutor>()
                       .AddScenarios(scenarioAssemblies);
    }

    private static IServiceCollection AddScenarios(this IServiceCollection services, Assembly[] scenarioAssemblies)
    {
        var scenarioTypes = from assembly in scenarioAssemblies
                            from type in assembly.GetTypes()
                            where type.IsClass && !type.IsAbstract && type.IsPublic
                            where type.GetInterfaces().Any(interfaceType => interfaceType == typeof(IScenario))
                            select type;

        foreach (var scenarioType in scenarioTypes)
        {
            services.AddTransient(scenarioType);
        }

        return services;
    }
}