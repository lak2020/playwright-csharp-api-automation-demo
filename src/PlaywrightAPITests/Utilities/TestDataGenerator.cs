using Bogus;

namespace PlaywrightAPITests.Utilities;

/// <summary>
/// Generates realistic random test data using Bogus library.
/// </summary>
public static class TestDataGenerator
{
    private static readonly Faker _faker = new();

    /// <summary>
    /// Generate a CreatePostRequest with random title, body, and userId.
    /// </summary>
    public static CreatePostRequest GenerateCreatePostRequest() => new()
    {
        Title = _faker.Lorem.Sentence(),
        Body = _faker.Lorem.Paragraphs(2),
        UserId = _faker.Random.Int(1, 10)
    };

    /// <summary>
    /// Generate an UpdatePostRequest with random title, body, and userId.
    /// </summary>
    public static UpdatePostRequest GenerateUpdatePostRequest() => new()
    {
        Title = _faker.Lorem.Sentence(),
        Body = _faker.Lorem.Paragraphs(2),
        UserId = _faker.Random.Int(1, 10)
    };

    /// <summary>
    /// Generate multiple CreatePostRequests for data-driven testing.
    /// </summary>
    public static IEnumerable<CreatePostRequest> GenerateMultiplePosts(int count) =>
        Enumerable.Range(0, count).Select(_ => GenerateCreatePostRequest());

    /// <summary>
    /// Generate a random email address.
    /// </summary>
    public static string GenerateEmail() => _faker.Internet.Email();

    /// <summary>
    /// Generate a random sentence for titles.
    /// </summary>
    public static string GenerateTitle() => _faker.Lorem.Sentence();
}
