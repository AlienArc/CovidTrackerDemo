﻿using CovidTracker.Core.Models;
using CovidTracker.Core.Repositories;

namespace CovidTracker.Web.ViewModels;

public class HomeViewModel(ICovidStateDataRepository dataRepository) : ViewModelBase
{
    private ICovidStateDataRepository DataRepository { get; } = dataRepository;
    public List<StateDailyTotal> DailyTotals { get; set; } = new();
    public DateTime? CurrentDate { get; set; } = new DateTime(2021, 1, 1);

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if(DailyTotals.Count == 0)
        {
            DailyTotals = await DataRepository.GetAllStateDataForDateAsync(new DateOnly(2021, 1, 1));
        }
    }
}