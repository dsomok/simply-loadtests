using LoadTesting.Tool.Measurement.Models;

namespace LoadTesting.Tool.Output;

public interface IOutputHelper
{
    Task PrintProgressAsync(ScenarioMeasurement scenarioMeasurement);
    Task PrintResultAsync(ScenarioMeasurement scenarioMeasurement);
}
