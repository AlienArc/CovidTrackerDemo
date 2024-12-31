using AutoMapper;
using CovidTracker.Core.Models;
using CovidTracker.Core.Repositories;

namespace CovidTracker.Web.ViewModels;

public class HomeViewModel(ICovidStateDataRepository dataRepository, IMapper mapper) : ViewModelBase
{
    private ICovidStateDataRepository DataRepository { get; } = dataRepository;
    private IMapper Mapper { get; } = mapper;
    public List<StateDailyTotalViewModel>? DailyTotals { get; set; } = new();
    public DateTime DefaultDate { get; set; } = new DateTime(2021, 3, 7);
    public DateTime? CurrentDate { get; set; }
    public DateTime? MaxDate { get; set; }
    public DateTime? MinDate { get; set; }

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    public async Task LoadDailyData()
    {
        var date = CurrentDate ?? DefaultDate;
        var totals = await DataRepository.GetAllStateDataForDateAsync(DateOnly.FromDateTime(date));
        DailyTotals = Mapper.Map<List<StateDailyTotal>, List<StateDailyTotalViewModel>>(totals);
    }

    public async Task LoadSummaryData()
    {
        var summary = await DataRepository.GetCovidDataSummary();
        MaxDate = summary.DateOfLatestRecord.ToDateTime(TimeOnly.MinValue);
        MinDate = summary.DateOfEaliestRecord.ToDateTime(TimeOnly.MinValue);
        if (CurrentDate == null || CurrentDate == DefaultDate)
        {
            CurrentDate = MaxDate;
        }
    }
}