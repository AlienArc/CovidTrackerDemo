namespace CovidTracker.Core.Tests.Mappers;

public class StateDailyTotalMapperTests : MapperTestBase<StateTotalMapper>
{
    [Test]
    public void ConvertsInputDate()
    {
        var source = new StateDailyTotalDto
        {
            date = 20240102,
            state = "Missouri",
            totalTestResults = 100,
            positive = 25,
            negative = 75,
            hospitalizedCurrently = 5
        };

        var result = Mapper.Map<StateDailyTotal>(source);

        result.Date.Should().Be(new DateOnly(2024, 1, 2));
    }
}
