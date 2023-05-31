using System.Reflection;
using LoadTesting.Tool.Scenario.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace LoadTesting.Tool.Scenario;

public static class Dependencies
{
    public static IServiceCollection AddScenarioDependencies(this IServiceCollection services, params Assembly[] scenarioAssemblies)
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