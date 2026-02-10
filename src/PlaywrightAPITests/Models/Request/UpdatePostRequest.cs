namespace PlaywrightAPITests.Models.Request;

/// <summary>
/// Request model for updating a post via PUT/PATCH /posts/{id}.
/// </summary>
public record UpdatePostRequest
{
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; init; } = string.Empty;

    [JsonPropertyName("userId")]
    public int UserId { get; init; }
}
