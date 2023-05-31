namespace LoadTesting.Tool.Measurement.Metrics;

public class TrendMetric
{
    private readonly IOrderedEnumerable<TimeSpan> _measurements;


    public TrendMetric(IOrderedEnumerable<TimeSpan> measurements)
    {
        _measurements = measurements;
    }

    public TimeSpan Average => TimeSpan.FromMilliseconds(_measurements.Average(measurement => measurement.TotalMilliseconds));
    public TimeSpan Min => TimeSpan.FromMilliseconds(_measurements.Min(measurement => measurement.TotalMilliseconds));
    public TimeSpan Max => TimeSpan.FromMilliseconds(_measurements.Max(measurement => measurement.TotalMilliseconds));
    public TimeSpan Median => Percentile(50);
    public TimeSpan Percentile(int percentile)
    {
        var index = (int)Math.Round((_measurements.Count() - 1) * (percentile / 100.0));
        return _measurements.ElementAt(index);
    }
}