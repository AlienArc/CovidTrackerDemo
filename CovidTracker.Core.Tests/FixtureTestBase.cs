namespace CovidTracker.Core.Tests;

[TestFixture]
public abstract class FixtureTestBase
{
    [SetUp]
    public void FixtureTestBaseSetup()
    {
        Fixture = new Fixture();
        Fixture.Customize(new AutoNSubstituteCustomization());
    }

    protected Fixture Fixture { get; private set; } = null!;
}
