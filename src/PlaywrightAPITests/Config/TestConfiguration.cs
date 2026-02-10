using Microsoft.Extensions.Configuration;

namespace PlaywrightAPITests.Config;

/// <summary>
/// Centralized configuration management using appsettings.json and environment variables.
/// </summary>
public static class TestConfiguration
{
    private static readonly IConfiguration _configuration;

    static TestConfiguration()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }

    public static string BaseUrl => _configuration["ApiSettings:BaseUrl"]
        ?? throw new InvalidOperationException("BaseUrl is not configured");

    public static int Timeout => int.Parse(_configuration["ApiSettings:Timeout"] ?? "30000");

    public static int RetryCount => int.Parse(_configuration["ApiSettings:RetryCount"] ?? "2");

    public static string LogDirectory => _configuration["Logging:LogDirectory"] ?? "logs";

    public static string LogLevel => _configuration["Logging:LogLevel"] ?? "Information";

    public static string GetCustomHeader(string key) =>
        _configuration[$"ApiSettings:Headers:{key}"] ?? string.Empty;
}
