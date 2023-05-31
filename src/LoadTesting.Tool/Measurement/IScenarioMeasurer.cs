using LoadTesting.Tool.Measurement.Models;

namespace LoadTesting.Tool.Measurement;

public interface IScenarioMeasurer
{
    ScenarioMeasurement CreateMeasurement(int vusCount);
}