using Serilog;

namespace PlaywrightAPITests.Utilities;

/// <summary>
/// Extension methods for enhanced API response assertions.
/// </summary>
public static class AssertionExtensions
{
    /// <summary>
    /// Assert that the response has a specific header value.
    /// </summary>
    public static void ShouldHaveHeader(this IAPIResponse response, string headerName, string expectedValue)
    {
        var headers = response.Headers;
        headers.Should().ContainKey(headerName.ToLower(),
            because: $"response should contain header '{headerName}'");
        headers[headerName.ToLower()].Should().Contain(expectedValue);
    }

    /// <summary>
    /// Assert that the response content type is JSON.
    /// </summary>
    public static void ShouldBeJson(this IAPIResponse response)
    {
        response.Headers.Should().ContainKey("content-type");
        response.Headers["content-type"].Should().Contain("application/json");
    }

    /// <summary>
    /// Assert that response was successful (2xx status code).
    /// </summary>
    public static void ShouldBeSuccessful(this IAPIResponse response)
    {
        response.Status.Should().BeInRange(200, 299,
            because: $"response should be successful but was {response.Status}");
    }

    /// <summary>
    /// Assert response body contains specific text.
    /// </summary>
    public static async Task ShouldContainTextAsync(this IAPIResponse response, string expectedText)
    {
        var body = await response.TextAsync();
        body.Should().Contain(expectedText,
            because: $"response body should contain '{expectedText}'");
    }

    /// <summary>
    /// Log and return response details for debugging.
    /// </summary>
    public static async Task<string> LogResponseAsync(this IAPIResponse response)
    {
        var body = await response.TextAsync();
        Log.Information("Response [{Status}]: {Body}", response.Status, body);
        return body;
    }
}
