namespace PlaywrightAPITests.Utilities;

/// <summary>
/// JSON serialization/deserialization helper with centralized configuration.
/// </summary>
public static class JsonHelper
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Deserialize JSON string to typed object.
    /// </summary>
    public static T Deserialize<T>(string json) =>
        JsonSerializer.Deserialize<T>(json, _options)
        ?? throw new JsonException($"Failed to deserialize JSON to {typeof(T).Name}");

    /// <summary>
    /// Serialize object to JSON string.
    /// </summary>
    public static string Serialize<T>(T obj) =>
        JsonSerializer.Serialize(obj, _options);

    /// <summary>
    /// Deserialize JSON to dynamic JsonElement for flexible assertions.
    /// </summary>
    public static JsonElement DeserializeToElement(string json) =>
        JsonDocument.Parse(json).RootElement;
}
