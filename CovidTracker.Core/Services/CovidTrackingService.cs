using AutoMapper;
using CovidTracker.Core.Dtos;
using CovidTracker.Core.Models;
using System.Text.Json;

namespace CovidTracker.Core.Services;

public class CovidTrackingService : ICovidTrackingService
{
    public CovidTrackingService(IMapper mapper)
    {
        Mapper = mapper;
    }

    public IMapper Mapper { get; }

    // get all states from rest api
    public async Task<List<StateDailyTotal>> GetStateData()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri($"https://api.covidtracking.com/v1/states/");
        var response = await client.GetAsync("daily.json");
        response.EnsureSuccessStatusCode();
        var stringResult = await response.Content.ReadAsStringAsync();
        var rawStateTotals = JsonSerializer.Deserialize<List<StateDailyTotalDto>>(stringResult) ?? new();
        var stateTotals = Mapper.Map<List<StateDailyTotalDto>, List<StateDailyTotal>>(rawStateTotals);
        return stateTotals;
    }
}
