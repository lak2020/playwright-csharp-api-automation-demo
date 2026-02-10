namespace PlaywrightAPITests.Models.Request;

/// <summary>
/// Request model for creating a new post via POST /posts.
/// </summary>
public record CreatePostRequest
{
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; init; } = string.Empty;

    [JsonPropertyName("userId")]
    public int UserId { get; init; }
}
