using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace CovidTracker.Web.Tests;

public class AutoFixtureRazorTestBase : NUnitTestContext
{
    [SetUp]
    public void AutoFixtureRazorTestBaseSetup()
    {
        Fixture = new Fixture();
        Fixture.Customize(new AutoFixture.AutoNSubstitute.AutoNSubstituteCustomization() { ConfigureMembers = true });
        Fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
    }

    public Fixture Fixture { get; private set; }

    protected void SetupMudBlazor(Bunit.TestContext context)
    {
        context.Services.AddMudServices();

        context.Services.AddSingleton<MudPopoverProvider>();

        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.JSInterop.SetupVoid("mudScrollManager.unlockScroll", _ => true);
        context.JSInterop.SetupVoid("mudPopover.initialize", _ => true);
        context.JSInterop.SetupVoid("mudKeyInterceptor.connect", _ => true);
    }
}
