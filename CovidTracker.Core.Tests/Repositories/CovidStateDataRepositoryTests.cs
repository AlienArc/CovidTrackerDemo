using CovidTracker.Core.Configuration;
using CovidTracker.Core.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CovidTracker.Core.Tests.Repositories;

public class CovidStateDataRepositoryTests : FixtureTestBase
{
    public List<StateDailyTotal> TrackingServiceStateData { get; set; }
    public List<StateDailyTotal>? MemoryCacheStateData { get; set; }
    public bool MemoryCacheResult { get; set; }
    public CacheOptions CacheOptions { get; set; }

    [SetUp]
    public void CovidStateDataRepositoryTestsSetup()
    {
        SetDefaultTestParameters();

        var covidTrackingService = Fixture.Freeze<ICovidTrackingService>();
        covidTrackingService.GetStateData()
            .Returns(ci => Task.FromResult(TrackingServiceStateData));

        var cacheOptions = Fixture.Freeze<IOptions<CacheOptions>>();
        cacheOptions.Value
            .Returns(ci => CacheOptions);

        var memoryCache = Fixture.Freeze<IMemoryCache>();
        memoryCache.TryGetValue(Arg.Any<string>(), out Arg.Any<List<StateDailyTotal>>())
            .Returns(ci =>
            {
                ci[1] = MemoryCacheStateData;
                return MemoryCacheResult;
            });
    }

    private void SetDefaultTestParameters()
    {
        TrackingServiceStateData = new List<StateDailyTotal>();
        CacheOptions = new CacheOptions { CacheDurationInMinutes = 1 };
        MemoryCacheStateData = null;
        MemoryCacheResult = false;
    }

    [Test]
    public async Task GetAllStateDataForDateAsync_NoCache_ReturnsApiData()
    {
        TrackingServiceStateData = Fixture.Create<List<StateDailyTotal>>();
        var date = TrackingServiceStateData.First().Date;

        var subject = Fixture.Create<CovidStateDataRepository>();

        var result = await subject.GetAllStateDataForDateAsync(date);

        result.Should().HaveCount(1);
    }

    [Test]
    public async Task GetAllStateDataForDateAsync_NoCache_OnlyReturnsValidDay()
    {
        TrackingServiceStateData = Fixture.Create<List<StateDailyTotal>>();
        var date = TrackingServiceStateData.First().Date;

        var subject = Fixture.Create<CovidStateDataRepository>();

        var result = await subject.GetAllStateDataForDateAsync(date);

        result.Should().HaveCount(1);
    }


    [Test]
    public async Task GetAllStateDataForDateAsync_NoCache_WritesServiceDataToCache()
    {
        TrackingServiceStateData = Fixture.Create<List<StateDailyTotal>>();
        var date = TrackingServiceStateData.First().Date;

        var subject = Fixture.Create<CovidStateDataRepository>();

        var result = await subject.GetAllStateDataForDateAsync(date);

        var memoryCache = Fixture.Create<IMemoryCache>();

        memoryCache.Received(1)
            .Set(CacheOptions.StateDataCacheKey, TrackingServiceStateData);
    }

    [Test]
    public async Task GetAllStateDataForDateAsync_Cache_OnlyReturnsCacheData()
    {

        var date = Fixture.Create<DateOnly>();

        TrackingServiceStateData = Fixture
            .Build<StateDailyTotal>()
            .With(o => o.Date, date.AddDays(1))
            .CreateMany(3)
            .ToList();

        MemoryCacheStateData = Fixture
            .Build<StateDailyTotal>()
            .With(o => o.Date, date)
            .CreateMany(3)
            .ToList();

        MemoryCacheResult = true;

        var subject = Fixture.Create<CovidStateDataRepository>();

        var result = await subject.GetAllStateDataForDateAsync(date);

        result.Should().HaveCount(3);
    }


}
