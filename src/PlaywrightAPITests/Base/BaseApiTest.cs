using Allure.NUnit;
using Serilog;

namespace PlaywrightAPITests.Base;

/// <summary>
/// Base test class providing shared setup/teardown for all API tests.
/// Manages Playwright lifecycle, logging, and common test infrastructure.
/// </summary>
[AllureNUnit]
[TestFixture]
public abstract class BaseApiTest
{
    protected ApiClientFactory ApiClient { get; private set; } = null!;
    protected IAPIRequestContext Request => ApiClient.RequestContext;

    [OneTimeSetUp]
    public virtual async Task OneTimeSetUp()
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(
                path: Path.Combine(TestConfiguration.LogDirectory, "test-log-.txt"),
                rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        ApiClient = new ApiClientFactory();
        await ApiClient.InitializeAsync();

        Log.Information("Test suite setup completed for: {TestClass}", GetType().Name);
    }

    [SetUp]
    public virtual void SetUp()
    {
        Log.Information("▶ Starting test: {TestName}", TestContext.CurrentContext.Test.Name);
    }

    [TearDown]
    public virtual void TearDown()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        var testName = TestContext.CurrentContext.Test.Name;

        if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            Log.Error("✘ Test FAILED: {TestName} — {Message}",
                testName, TestContext.CurrentContext.Result.Message);
        }
        else
        {
            Log.Information("✔ Test PASSED: {TestName}", testName);
        }
    }

    [OneTimeTearDown]
    public virtual async Task OneTimeTearDown()
    {
        await ApiClient.DisposeAsync();
        await Log.CloseAndFlushAsync();
    }

    /// <summary>
    /// Helper to deserialize API response body into a typed object.
    /// </summary>
    protected async Task<T> DeserializeResponseAsync<T>(IAPIResponse response)
    {
        var body = await response.TextAsync();
        Log.Debug("Response body: {Body}", body);

        return JsonHelper.Deserialize<T>(body);
    }

    /// <summary>
    /// Helper to log and assert HTTP status code.
    /// </summary>
    protected void AssertStatusCode(IAPIResponse response, int expectedStatusCode)
    {
        Log.Information("Response status: {StatusCode} (expected: {Expected})", response.Status, expectedStatusCode);
        response.Status.Should().Be(expectedStatusCode,
            because: $"the API should return {expectedStatusCode} but returned {response.Status}");
    }
}
