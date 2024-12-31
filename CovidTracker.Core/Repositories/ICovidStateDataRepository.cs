using CovidTracker.Core.Models;

namespace CovidTracker.Core.Repositories;

public interface ICovidStateDataRepository
{
    Task<List<StateDailyTotal>> GetAllStateDataForDateAsync(DateOnly date);
    Task<CovidDataSummary> GetCovidDataSummary();
}
