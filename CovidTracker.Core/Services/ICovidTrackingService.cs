using CovidTracker.Core.Models;

namespace CovidTracker.Core.Services;

public interface ICovidTrackingService
{
    Task<List<StateDailyTotal>> GetStateData();
}
