using AutoMapper;
using CovidTracker.Core.Dtos;
using CovidTracker.Core.Models;

namespace CovidTracker.Core.Mappers;

public class StateTotalMapper : Profile
{
    public StateTotalMapper()
    {
        CreateMap<StateDailyTotalDto, StateDailyTotal>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.ParseExact(src.date.ToString(), "yyyyMMdd")))
            .ForMember(dest => dest.TotalCases, opt => opt.MapFrom(src => src.totalTestResults))
            .ForMember(dest => dest.PositiveCases, opt => opt.MapFrom(src => src.positive))
            .ForMember(dest => dest.NegativeCases, opt => opt.MapFrom(src => src.negative))
            .ForMember(dest => dest.Hospitalized, opt => opt.MapFrom(src => src.hospitalizedCurrently));
    }
}

