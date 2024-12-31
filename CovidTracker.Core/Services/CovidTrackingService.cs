using AutoMapper;
using CovidTracker.Core.Dtos;
using CovidTracker.Core.Models;
using System.Net.Http;
using System.Text.Json;

namespace CovidTracker.Core.Services;

public class CovidTrackingService : ICovidTrackingService
{
    public CovidTrackingService(IMapper mapper, IHttpClientFactory httpClientFactory)
    {
        Mapper = mapper;
        HttpClientFactory = httpClientFactory;
    }

    private IMapper Mapper { get; }
    private IHttpClientFactory HttpClientFactory { get; }

    // get all states from rest api
    public async Task<List<StateDailyTotal>> GetStateData()
    {
        using var client = HttpClientFactory.CreateClient();
        client.BaseAddress = new Uri($"https://api.covidtracking.com/v1/states/");
        var response = await client.GetAsync("daily.json");
        response.EnsureSuccessStatusCode();
        var stringResult = await response.Content.ReadAsStringAsync();
        var rawStateTotals = JsonSerializer.Deserialize<List<StateDailyTotalDto>>(stringResult) ?? new();
        var stateTotals = Mapper.Map<List<StateDailyTotalDto>, List<StateDailyTotal>>(rawStateTotals);
        return stateTotals;
    }
}
