using System.Diagnostics;

namespace LoadTesting.Tool.Measurement.Models;

public class IterationMeasurement : IDisposable
{
    private readonly ScenarioMeasurement _scenarioMeasurement;
    private readonly Stopwatch _stopwatch;

    public IterationMeasurement(ScenarioMeasurement scenarioMeasurement)
    {
        _scenarioMeasurement = scenarioMeasurement;
        _stopwatch = Stopwatch.StartNew();
        Succeeded = true;
    }

    public TimeSpan Elapsed { get; private set; }

    public bool Succeeded { get; private set; }

    public Exception? Exception { get; private set; }

    public void SetException(Exception exception)
    {
        Exception = exception;
        Succeeded = false;
    }

    public void Dispose()
    {
        _stopwatch.Stop();
        Elapsed = _stopwatch.Elapsed;

        _scenarioMeasurement.RecordIterationMeasurement(this);
    }
}