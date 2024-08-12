using Simply.LoadTests.Measurement.Models;

namespace Simply.LoadTests.Output;

public interface IOutputHelper
{
    Task PrintProgressAsync(ScenarioMeasurement scenarioMeasurement);
    Task PrintResultAsync(ScenarioMeasurement scenarioMeasurement);
}
