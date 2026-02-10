using Serilog;

namespace PlaywrightAPITests.Base;

/// <summary>
/// Factory for creating and managing Playwright API request contexts.
/// Provides centralized configuration for all API calls.
/// </summary>
public class ApiClientFactory : IAsyncDisposable
{
    private IPlaywright? _playwright;
    private IAPIRequestContext? _requestContext;

    public IAPIRequestContext RequestContext => _requestContext
        ?? throw new InvalidOperationException("API client not initialized. Call InitializeAsync() first.");

    /// <summary>
    /// Initializes Playwright and creates an API request context with default configuration.
    /// </summary>
    public async Task InitializeAsync(Dictionary<string, string>? extraHeaders = null)
    {
        _playwright = await Playwright.CreateAsync();

        var headers = new Dictionary<string, string>
        {
            ["Accept"] = "application/json",
            ["Content-Type"] = "application/json"
        };

        if (extraHeaders != null)
        {
            foreach (var header in extraHeaders)
            {
                headers[header.Key] = header.Value;
            }
        }

        _requestContext = await _playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = TestConfiguration.BaseUrl,
            ExtraHTTPHeaders = headers,
            IgnoreHTTPSErrors = true
        });

        Log.Information("API client initialized with BaseURL: {BaseUrl}", TestConfiguration.BaseUrl);
    }

    /// <summary>
    /// Creates a new API request context with custom base URL (useful for testing different services).
    /// </summary>
    public async Task<IAPIRequestContext> CreateContextAsync(string baseUrl, Dictionary<string, string>? headers = null)
    {
        _playwright ??= await Playwright.CreateAsync();

        return await _playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = baseUrl,
            ExtraHTTPHeaders = headers,
            IgnoreHTTPSErrors = true
        });
    }

    public async ValueTask DisposeAsync()
    {
        if (_requestContext != null)
        {
            await _requestContext.DisposeAsync();
            _requestContext = null;
        }

        _playwright?.Dispose();
        _playwright = null;

        Log.Information("API client disposed");
        GC.SuppressFinalize(this);
    }
}
