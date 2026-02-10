namespace PlaywrightAPITests.Models.Response;

/// <summary>
/// Response model for a post from GET /posts/{id} or POST /posts.
/// JSONPlaceholder returns flat objects (no wrapper).
/// </summary>
public record PostResponse
{
    [JsonPropertyName("userId")]
    public int UserId { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; init; } = string.Empty;
}
