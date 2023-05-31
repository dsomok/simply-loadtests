namespace LoadTesting.Tool.Scenario.Runner;

public interface IScenarioRunnerFactory
{
    IScenarioRunner CreateConcurrentUsersScenarioRunner(int concurrentUsersCount, int iterationsCount);
}