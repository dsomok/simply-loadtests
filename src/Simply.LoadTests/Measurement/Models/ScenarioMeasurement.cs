using System.Collections.Concurrent;
using System.Diagnostics;
using Simply.LoadTests.Measurement.Metrics;

namespace Simply.LoadTests.Measurement.Models;

public class ScenarioMeasurement
{
    private readonly Stopwatch _stopwatch;
    private readonly ConcurrentBag<IterationMeasurement> _iterations;

    private bool _isStopped;

    public ScenarioMeasurement(int vusCount)
    {
        VUsCount = vusCount;
        _stopwatch = new Stopwatch();
        _iterations = new ConcurrentBag<IterationMeasurement>();

        _isStopped = false;
    }

    public int VUsCount { get; }
    public int TotalIterationsCount => _iterations.Count;
    public int SucceededIterationsCount => _iterations.Count(i => i.Succeeded);
    public int FailedIterationsCount => _iterations.Count(i => !i.Succeeded);
    public TimeSpan TotalElapsed => _stopwatch.Elapsed;
    public int RequestRate => TotalElapsed.TotalSeconds != 0 ? (int)(TotalIterationsCount / TotalElapsed.TotalSeconds) : 0;

    public TrendMetric IterationDuration => new(_iterations.Select(i => i.Elapsed).OrderBy(i => i));

    public IList<Exception> Exceptions => _iterations.Where(i => i.Exception != null).Select(i => i.Exception!).ToList();

    public void Start()
    {
        _stopwatch.Start();
    }

    public void Stop()
    {
        _stopwatch.Stop();
        _isStopped = true;
    }

    public IterationMeasurement MeasureIteration()
    {
        if (_isStopped)
        {
            throw new InvalidOperationException("ScenarioMeasurement is stopped.");
        }

        return new IterationMeasurement(this);
    }

    internal void RecordIterationMeasurement(IterationMeasurement iterationMeasurement)
    {
        _iterations.Add(iterationMeasurement);
    }
}