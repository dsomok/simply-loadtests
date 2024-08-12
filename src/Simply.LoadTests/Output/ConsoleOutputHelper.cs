using ConsoleTables;
using Simply.LoadTests.Measurement.Metrics;
using Simply.LoadTests.Measurement.Models;

namespace Simply.LoadTests.Output;

internal class ConsoleOutputHelper : IOutputHelper
{
    private const int METRIC_NAME_LENGTH = 20;

    public Task PrintProgressAsync(ScenarioMeasurement scenarioMeasurement)
    {
        Console.WriteLine($"running ({scenarioMeasurement.TotalElapsed.Minutes}m{scenarioMeasurement.TotalElapsed.Seconds}s), {scenarioMeasurement.RequestRate} iterations/s, " +
                          $"{scenarioMeasurement.SucceededIterationsCount} complete and {scenarioMeasurement.FailedIterationsCount} interrupted iterations");

        return Task.CompletedTask;
    }

    public Task PrintResultAsync(ScenarioMeasurement scenarioMeasurement)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("RESULTS");
        Console.WriteLine();

        PrintMetric("vus", $"{scenarioMeasurement.VUsCount} users");
        PrintMetric("elapsed", scenarioMeasurement.TotalElapsed.ToString());
        PrintMetric("iterations_count", $"{scenarioMeasurement.TotalIterationsCount} (succeeded={scenarioMeasurement.SucceededIterationsCount} failed={scenarioMeasurement.FailedIterationsCount})");
        PrintMetric("iterations_duration", scenarioMeasurement.IterationDuration);
        PrintMetric("iterations_rate", $"{scenarioMeasurement.RequestRate} iterations/s");

        Console.WriteLine();
        Console.WriteLine();

        PrintExceptions(scenarioMeasurement);

        return Task.CompletedTask;
    }

    private void PrintMetric(string metricName, string value)
    {
        if (metricName.Length >= METRIC_NAME_LENGTH)
        {
            throw new ArgumentException("Metric name is too long", nameof(metricName));
        }

        Console.WriteLine($"{metricName}{new string('.', METRIC_NAME_LENGTH - metricName.Length)}: {value}");
    }

    private void PrintMetric(string metricName, TrendMetric value)
    {
        var metricValue = $"avg={value.Average.TotalMilliseconds}ms " +
                          $"min={value.Min.TotalMilliseconds}ms " +
                          $"med={value.Median.TotalMilliseconds}ms " +
                          $"max={value.Max.TotalMilliseconds}ms " +
                          $"p(90)={value.Percentile(90).TotalMilliseconds}ms " +
                          $"p(95)={value.Percentile(95).TotalMilliseconds}ms";

        PrintMetric(metricName, metricValue);
    }

    private void PrintExceptions(ScenarioMeasurement scenarioMeasurement)
    {
        if (!scenarioMeasurement.Exceptions.Any())
        {
            return;
        }

        var exceptions = scenarioMeasurement.Exceptions.GroupBy(e => (Type: e.GetType(), Message: e.Message));

        Console.WriteLine("ERRORS");
        Console.WriteLine();

        var table = new ConsoleTable("Type", "Message", "Occurrences");
        foreach (var exception in exceptions)
        {
            table.AddRow(exception.Key.Type.Name, exception.Key.Message, exception.Count());
        }

        table.Write();
    }
}