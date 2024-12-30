namespace CovidTracker.Core.Models;

public class StateDailyTotal
{
    public DateOnly Date { get; set; }
    public required string State { get; set; }
    public int TotalCases { get; set; }
    public int PositiveCases { get; set; }
    public int NegativeCases { get; set; }
    public int Hospitalized { get; set; }
}
