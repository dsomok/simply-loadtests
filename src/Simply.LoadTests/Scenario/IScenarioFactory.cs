namespace Simply.LoadTests.Scenario;

internal interface IScenarioFactory
{
    IScenario CreateScenario<TScenario>()
        where TScenario : IScenario;
}