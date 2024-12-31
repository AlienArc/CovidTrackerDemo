namespace CovidTracker.Core.Models;

public class CovidDataSummary
{
    public DateOnly DateOfEaliestRecord { get; set; }
    public DateOnly DateOfLatestRecord { get; set; }
}
