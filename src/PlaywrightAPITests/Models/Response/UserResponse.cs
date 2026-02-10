namespace PlaywrightAPITests.Models.Response;

/// <summary>
/// Response model for a user from GET /users/{id}.
/// JSONPlaceholder user has nested address, company, and geo objects.
/// </summary>
public record UserResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; init; } = string.Empty;

    [JsonPropertyName("website")]
    public string Website { get; init; } = string.Empty;

    [JsonPropertyName("address")]
    public AddressInfo? Address { get; init; }

    [JsonPropertyName("company")]
    public CompanyInfo? Company { get; init; }
}

public record AddressInfo
{
    [JsonPropertyName("street")]
    public string Street { get; init; } = string.Empty;

    [JsonPropertyName("suite")]
    public string Suite { get; init; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; init; } = string.Empty;

    [JsonPropertyName("zipcode")]
    public string Zipcode { get; init; } = string.Empty;
}

public record CompanyInfo
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("catchPhrase")]
    public string CatchPhrase { get; init; } = string.Empty;

    [JsonPropertyName("bs")]
    public string Bs { get; init; } = string.Empty;
}
