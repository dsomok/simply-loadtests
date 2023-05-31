namespace LoadTesting.Tool.Scenario;

internal interface IScenarioFactory
{
    IScenario CreateScenario<TScenario>()
        where TScenario : IScenario;
}