using Simply.LoadTests.Measurement.Models;

namespace Simply.LoadTests.Measurement;

public interface IScenarioMeasurer
{
    ScenarioMeasurement CreateMeasurement(int vusCount);
}