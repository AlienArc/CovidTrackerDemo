namespace CovidTracker.Web.Tests;

public abstract class NUnitTestContext : TestContextWrapper
{
    [SetUp]
    public void NUnitTestContextSetup() => TestContext = new Bunit.TestContext();

    [TearDown]
    public void NUnitTestContextTearDown() => TestContext?.Dispose();
}
