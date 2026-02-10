namespace PlaywrightAPITests.Models.Request;

/// <summary>
/// Request model for updating a user via PUT/PATCH /users/{id}.
/// </summary>
public record UpdateUserRequest
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("job")]
    public string Job { get; init; } = string.Empty;
}
