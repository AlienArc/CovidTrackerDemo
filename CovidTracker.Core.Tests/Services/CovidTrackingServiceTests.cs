using AutoMapper;

namespace CovidTracker.Core.Tests.Services;

public class CovidTrackingServiceTests : FixtureTestBase
{

    public HttpResponseMessage? ResponseMessage { get; set; }
    public List<StateDailyTotal>? MapResult { get; set; }

    [SetUp]
    public void CovidTrackingServiceTestsSetup()
    {
        SetDefaultTestParameters();

        var mapper = Fixture.Freeze<IMapper>();
        mapper.Map<List<StateDailyTotalDto>, List<StateDailyTotal>>(Arg.Any<List<StateDailyTotalDto>>())
            .Returns(ci => MapResult);

        var chf = Fixture.Freeze<IHttpClientFactory>();
        chf.CreateClient()
            .Returns(ci => new HttpClient(new MockHttpMessageHandler(ResponseMessage!)));
    }

    private void SetDefaultTestParameters()
    {
        ResponseMessage = new HttpResponseMessage()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent("[]")
        };

        MapResult = new List<StateDailyTotal>();
    }

    [TearDown]
    public void CovidTrackingServiceTestsTearDown()
    {
        ResponseMessage?.Dispose();
        ResponseMessage = null;
    }

    [Test]
    public async Task GetStateData_ReturnsMapResult()
    {
        MapResult = new List<StateDailyTotal>
        {
            new StateDailyTotal
            {
                Date = new DateOnly(2024, 01, 02),
                State = "Missouri",
                TotalCases = 100,
                PositiveCases = 25,
                NegativeCases = 75,
                Hospitalized = 5,
            }
        };

        var subject = Fixture.Create<CovidTrackingService>();

        var result = await subject.GetStateData();

        result.Should().HaveCount(1);
        result.First().Date.Should().Be(new DateOnly(2024, 01, 02));
        result.First().State.Should().Be("Missouri");
        result.First().TotalCases.Should().Be(100);
        result.First().PositiveCases.Should().Be(25);
        result.First().NegativeCases.Should().Be(75);
        result.First().Hospitalized.Should().Be(5);

    }

    [Test]
    public async Task GetStateData_PassesHttpResultToMapper()
    {
        ResponseMessage = new HttpResponseMessage()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent("""
                [
                {
                    "date": 20240102,
                    "state": "Missouri",
                    "totalTestResults": 100,
                    "positive": 25,
                    "negative": 75,
                    "hospitalizedCurrently": 5
                }
                ]
                """)
        };

        var subject = Fixture.Create<CovidTrackingService>();

        var result = await subject.GetStateData();

        var mapper = Fixture.Create<IMapper>();
        mapper.Received(1)
            .Map<List<StateDailyTotalDto>, List<StateDailyTotal>>(Arg.Is<List<StateDailyTotalDto>>(v => v.First().date.Equals(20240102)));
    }

    [Test]
    public async Task GetStateData_CallsMapperMap()
    {
        var subject = Fixture.Create<CovidTrackingService>();

        var result = await subject.GetStateData();

        var mapper = Fixture.Create<IMapper>();
        mapper.Received(1)
            .Map<List<StateDailyTotalDto>, List<StateDailyTotal>>(Arg.Any<List<StateDailyTotalDto>>());
    }
}
