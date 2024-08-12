namespace Simply.LoadTests.Scenario.Runner;

public interface IScenarioRunnerFactory
{
    IScenarioRunner CreateConcurrentUsersScenarioRunner(int concurrentUsersCount, int iterationsCount);
}