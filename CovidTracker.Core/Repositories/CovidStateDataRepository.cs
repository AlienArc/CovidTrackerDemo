using CovidTracker.Core.Configuration;
using CovidTracker.Core.Models;
using CovidTracker.Core.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CovidTracker.Core.Repositories;

public class CovidStateDataRepository : ICovidStateDataRepository
{
    public CovidStateDataRepository(ICovidTrackingService covidTrackingService, IOptions<CacheOptions> cacheOptions, IMemoryCache memoryCache)
    {
        CovidTrackingService = covidTrackingService;
        CacheSettings = cacheOptions.Value;
        MemoryCache = memoryCache;
    }

    private ICovidTrackingService CovidTrackingService { get; }
    private CacheOptions CacheSettings { get; }
    private IMemoryCache MemoryCache { get; }

    public async Task<List<StateDailyTotal>> GetAllStateDataForDateAsync(DateOnly date)
    {
        var allCachedData = await GetCachedData();
        return allCachedData.Where(x => x.Date == date).ToList();
    }

    public async Task<CovidDataSummary> GetCovidDataSummary()
    {
        var allCachedData = await GetCachedData();
        return new CovidDataSummary
        {
            DateOfEaliestRecord = allCachedData.Min(r => r.Date),
            DateOfLatestRecord = allCachedData.Max(r => r.Date),
            States = allCachedData.Select(r => r.State).Distinct().Order().ToList()
        };
    }

    private async Task<List<StateDailyTotal>> GetCachedData()
    {
        // pull the cached data to make sure it hasn't expired
        MemoryCache.TryGetValue<List<StateDailyTotal>>(CacheSettings.StateDataCacheKey, out var cachedData);

        if (cachedData == null)
        {
            if (cachedData == null)
            {
                cachedData = await CovidTrackingService.GetStateData();
                MemoryCache.Set(CacheSettings.StateDataCacheKey, cachedData, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheSettings.CacheDurationInMinutes)
                });
            }
        }

        return cachedData;
    }
}
