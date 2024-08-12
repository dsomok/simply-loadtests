using Simply.LoadTests.Measurement;
using Simply.LoadTests.Measurement.Models;

namespace Simply.LoadTests.Scenario.Runner.Concrete;

internal class ConcurrentUsersScenarioRunner : BaseScenarioRunner
{
    private readonly int _iterationsCount;

    public ConcurrentUsersScenarioRunner(
        int vusCount, 
        int iterationsCount, 
        IScenarioMeasurer scenarioMeasurer, 
        IScenarioFactory scenarioFactory
    ) : base(vusCount, scenarioMeasurer, scenarioFactory)
    {
        _iterationsCount = iterationsCount;
    }

    protected override async Task<ScenarioMeasurement> RunScenarioAsync(IScenario scenario, CancellationToken cancellationToken)
    {
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = VusCount,
            CancellationToken = cancellationToken
        };
            
        ScenarioMeasurement.Start();

        try
        {
            await Parallel.ForEachAsync(Enumerable.Range(0, _iterationsCount), parallelOptions, async (iteration, ct) =>
            {
                if (ct.IsCancellationRequested)
                {
                    return;
                }

                var scenarioContext = new ScenarioContext(iteration);

                await scenario.BeforeScenarioAsync(scenarioContext, ct);

                using (var iterationMeasurement = ScenarioMeasurement.MeasureIteration())
                {
                    try
                    {
                        await scenario.ExecuteAsync(scenarioContext, ct);
                    }
                    catch (Exception ex)
                    {
                        iterationMeasurement.SetException(ex);
                    }
                }

                await scenario.AfterScenarioAsync(scenarioContext, ct);
            });
        }
        catch (OperationCanceledException)
        {
        }

        ScenarioMeasurement.Stop();

        return ScenarioMeasurement;
    }
}