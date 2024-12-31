using AutoMapper;
using CovidTracker.Core.Models;
using CovidTracker.Web.ViewModels;

namespace CovidTracker.Web.Mappers;

public class StateDailyTotalViewModelMapper : Profile
{
    public StateDailyTotalViewModelMapper()
    {
        CreateMap<StateDailyTotal, StateDailyTotalViewModel>();
    }
}
