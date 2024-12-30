using CovidTracker.Core.Models;
using CovidTracker.Core.Services;

namespace CovidTracker.Core.Repositories;

public class CovidStateDataRepository : ICovidStateDataRepository
{
    public CovidStateDataRepository(ICovidTrackingService covidTrackingService)
    {
        CovidTrackingService = covidTrackingService;
    }

    private const int CacheDurationInMinutes = 24 * 60; // Once a day, but is there a reason to ever expire this cache?
    private ICovidTrackingService CovidTrackingService { get; }
    private List<StateDailyTotal>? AllStateData { get; set; }
    private DateTime? LastRetrieved { get; set; }

    public async Task<List<StateDailyTotal>> GetAllStateDataForDateAsync(DateOnly date)
    {
        if (AllStateData == null || LastRetrieved?.AddMinutes(CacheDurationInMinutes) < DateTime.Now)
        {
            AllStateData = await CovidTrackingService.GetStateData();
            LastRetrieved = DateTime.Now;
        }

        return AllStateData.Where(x => x.Date == date).ToList();
    }
}
