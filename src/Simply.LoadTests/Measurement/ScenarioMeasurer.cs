using Simply.LoadTests.Measurement.Models;

namespace Simply.LoadTests.Measurement;

internal class ScenarioMeasurer : IScenarioMeasurer
{
    public ScenarioMeasurement CreateMeasurement(int vusCount)
    {
        return new ScenarioMeasurement(vusCount);
    }
}