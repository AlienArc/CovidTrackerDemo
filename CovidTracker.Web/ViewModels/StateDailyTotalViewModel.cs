namespace CovidTracker.Web.ViewModels;

public class StateDailyTotalViewModel
{
    public DateOnly Date { get; set; }
    public required string State { get; set; }
    public int TotalCases { get; set; }
    public string TotalCasesFormated => TotalCases.ToString("N0");
    public int PositiveCases { get; set; }
    public string PositiveCasesFormated => PositiveCases.ToString("N0");
    public int NegativeCases { get; set; }
    public string NegativeCasesFormated => NegativeCases.ToString("N0");
    public int Hospitalized { get; set; }
    public string HospitalizedFormated => Hospitalized.ToString("N0");
    public decimal HospitalizationRate => PositiveCases != 0 ? (decimal)Hospitalized/PositiveCases : 0;
    public string HospitalizationRateFormated => HospitalizationRate.ToString("P2");
}
