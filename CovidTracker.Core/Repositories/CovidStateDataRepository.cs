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
        // pull the cached data to make sure it hasn't expired
        MemoryCache.TryGetValue<List<StateDailyTotal>>(CacheSettings.StateDataCacheKey, out var cachedData);

        if (cachedData == null)
        {
            await Task.Delay(2000); //TODO: Remove this line
            cachedData = await CovidTrackingService.GetStateData();
            MemoryCache.Set(CacheSettings.StateDataCacheKey, cachedData, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheSettings.CacheDurationInMinutes)
            });
        }

        return cachedData.Where(x => x.Date == date).ToList();
    }
}
