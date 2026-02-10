namespace PlaywrightAPITests.Models.Response;

/// <summary>
/// Response model for a comment from GET /comments or /posts/{id}/comments.
/// </summary>
public record CommentResponse
{
    [JsonPropertyName("postId")]
    public int PostId { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; init; } = string.Empty;
}
