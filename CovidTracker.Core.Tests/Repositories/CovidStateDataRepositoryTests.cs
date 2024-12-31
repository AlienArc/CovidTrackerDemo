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
        TrackingServiceStateData = new List<StateDailyTotal>();
        CacheOptions = new CacheOptions { CacheDurationInMinutes = 1 };
        MemoryCacheStateData = null;
        MemoryCacheResult = false;

        var cts = Fixture.Freeze<ICovidTrackingService>();
        cts.GetStateData()
            .Returns(ci => Task.FromResult(TrackingServiceStateData));

        var co = Fixture.Freeze<IOptions<CacheOptions>>();
        co.Value.Returns(ci => CacheOptions);

        var mc = Fixture.Freeze<IMemoryCache>();
        mc.TryGetValue(Arg.Any<string>(), out Arg.Any<List<StateDailyTotal>>())
            .Returns(ci =>
            {
                ci[1] = MemoryCacheStateData;
                return MemoryCacheResult;
            });

    }

    [Test]
    public async Task GetAllStateDataForDateAsync_NoCache_ReturnsApiData()
    {
        var date = DateOnly.FromDateTime(Fixture.Create<DateTime>());
        TrackingServiceStateData = new List<StateDailyTotal>
        {
            new StateDailyTotal
            { 
                Date = date,
                State = "Missouri"
            }
        };

        var subject = Fixture.Create<CovidStateDataRepository>();

        var result = await subject.GetAllStateDataForDateAsync(date);

        result.Should().HaveCount(1);
    }

    [Test]
    public async Task GetAllStateDataForDateAsync_NoCache_OnlyReturnsValidDay()
    {
        var date = DateOnly.FromDateTime(Fixture.Create<DateTime>());
        TrackingServiceStateData = new List<StateDailyTotal>
        {
            new StateDailyTotal
            {
                Date = date,
                State = "Missouri"
            },
            new StateDailyTotal
            {
                Date = date.AddDays(-1),
                State = "Missouri"
            },
            new StateDailyTotal
            {
                Date = date.AddDays(1),
                State = "Missouri"
            }
        };

        var subject = Fixture.Create<CovidStateDataRepository>();

        var result = await subject.GetAllStateDataForDateAsync(date);

        result.Should().HaveCount(1);
    }


    [Test]
    public async Task GetAllStateDataForDateAsync_NoCache_WritesServiceDataToCache()
    {
        var date = DateOnly.FromDateTime(Fixture.Create<DateTime>());
        TrackingServiceStateData = new List<StateDailyTotal>
        {
            new StateDailyTotal
            {
                Date = date,
                State = "Missouri"
            },
            new StateDailyTotal
            {
                Date = date.AddDays(-1),
                State = "Missouri"
            },
            new StateDailyTotal
            {
                Date = date.AddDays(1),
                State = "Missouri"
            }
        };

        var subject = Fixture.Create<CovidStateDataRepository>();

        var result = await subject.GetAllStateDataForDateAsync(date);

        var memoryCache = Fixture.Create<IMemoryCache>();

        memoryCache.Received(1)
            .Set(CacheOptions.StateDataCacheKey, TrackingServiceStateData);
    }

    [Test]
    public async Task GetAllStateDataForDateAsync_Cache_OnlyReturnsCacheData()
    {
        var date = DateOnly.FromDateTime(Fixture.Create<DateTime>());
        TrackingServiceStateData = new List<StateDailyTotal>
        {
            new StateDailyTotal
            {
                Date = date,
                State = "Missouri"
            },
            new StateDailyTotal
            {
                Date = date,
                State = "Missouri"
            },
            new StateDailyTotal
            {
                Date = date,
                State = "Missouri"
            }
        };
        MemoryCacheStateData = new List<StateDailyTotal>
        {
            new StateDailyTotal
            {
                Date = date,
                State = "Missouri"
            },
            new StateDailyTotal
            {
                Date = date.AddDays(-1),
                State = "Missouri"
            },
            new StateDailyTotal
            {
                Date = date.AddDays(1),
                State = "Missouri"
            }
        };
        MemoryCacheResult = true;

        var subject = Fixture.Create<CovidStateDataRepository>();

        var result = await subject.GetAllStateDataForDateAsync(date);

        result.Should().HaveCount(1);
    }


}
