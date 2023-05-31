using LoadTesting.Tool.Measurement.Models;

namespace LoadTesting.Tool.Measurement;

internal class ScenarioMeasurer : IScenarioMeasurer
{
    public ScenarioMeasurement CreateMeasurement(int vusCount)
    {
        return new ScenarioMeasurement(vusCount);
    }
}