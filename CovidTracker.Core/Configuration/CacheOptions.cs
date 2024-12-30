namespace CovidTracker.Core.Configuration;

public class CacheOptions
{
    public readonly string StateDataCacheKey = "StateDataCacheKey";
    public int CacheDurationInMinutes { get; set; } = 60 * 24; // Defaults to one day
}
