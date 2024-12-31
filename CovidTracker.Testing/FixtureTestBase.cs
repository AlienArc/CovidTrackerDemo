namespace CovidTracker.Testing;

[TestFixture]
public abstract class FixtureTestBase
{
    [SetUp]
    public void FixtureTestBaseSetup()
    {
        Fixture = new Fixture();
        Fixture.Customize(new AutoNSubstituteCustomization());
        Fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
    }

    protected Fixture Fixture { get; private set; } = null!;
}
