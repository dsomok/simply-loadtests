using LoadTesting.Tool.Output;
using Microsoft.Extensions.DependencyInjection;

namespace LoadTesting.Tool.Measurement;

public static class Dependencies
{
    public static IServiceCollection AddScenarioMeasurementDependencies(this IServiceCollection services)
    {
        return services.AddSingleton<IScenarioMeasurer, ScenarioMeasurer>()
                       .AddSingleton<IOutputHelper, ConsoleOutputHelper>();
    }
}