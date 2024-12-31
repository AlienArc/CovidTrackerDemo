namespace CovidTracker.Testing;

public class MockHttpMessageHandler : HttpMessageHandler
{
    private HttpResponseMessage ResponseMessage { get; }

    public MockHttpMessageHandler(HttpResponseMessage responseMessage)
    {
        ResponseMessage = responseMessage;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResponseMessage);
    }
}
