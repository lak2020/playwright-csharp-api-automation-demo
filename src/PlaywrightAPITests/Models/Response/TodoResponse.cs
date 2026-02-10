namespace PlaywrightAPITests.Models.Response;

/// <summary>
/// Response model for a todo from GET /todos or /todos/{id}.
/// </summary>
public record TodoResponse
{
    [JsonPropertyName("userId")]
    public int UserId { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("completed")]
    public bool Completed { get; init; }
}
