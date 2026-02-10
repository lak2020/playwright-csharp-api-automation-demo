using Allure.Net.Commons;

namespace PlaywrightAPITests.Tests.PostTests;

/// <summary>
/// Tests for POST /posts endpoint.
/// Covers: creating posts with valid data, random data, and data-driven tests.
/// </summary>
[TestFixture]
[Category("Posts")]
[Category("POST")]
[AllureSuite("Post Management")]
[AllureSubSuite("Create Posts")]
public class CreatePostTests : BaseApiTest
{
    private PostApiService _postService = null!;

    [OneTimeSetUp]
    public override async Task OneTimeSetUp()
    {
        await base.OneTimeSetUp();
        _postService = new PostApiService(Request);
    }

    [Test]
    [AllureTag("Smoke")]
    [Description("Verify POST /posts creates a new post and returns 201")]
    public async Task CreatePost_WithValidData_Returns201()
    {
        // Arrange
        var newPost = new CreatePostRequest
        {
            Title = "Test Post Title",
            Body = "This is the body of the test post.",
            UserId = 1
        };

        // Act
        var response = await _postService.CreatePostAsync(newPost);

        // Assert
        AssertStatusCode(response, 201);

        var result = await DeserializeResponseAsync<PostResponse>(response);
        result.Title.Should().Be(newPost.Title);
        result.Body.Should().Be(newPost.Body);
        result.UserId.Should().Be(newPost.UserId);
        result.Id.Should().BeGreaterThan(0);
    }

    [Test]
    [AllureTag("Smoke")]
    [Description("Verify POST /posts with Bogus random data creates post correctly")]
    public async Task CreatePost_WithRandomData_Returns201()
    {
        // Arrange
        var newPost = TestDataGenerator.GenerateCreatePostRequest();

        // Act
        var response = await _postService.CreatePostAsync(newPost);

        // Assert
        AssertStatusCode(response, 201);

        var result = await DeserializeResponseAsync<PostResponse>(response);
        result.Title.Should().Be(newPost.Title);
        result.Body.Should().Be(newPost.Body);
        result.UserId.Should().Be(newPost.UserId);
    }

    [TestCase("First Post", "Body of first post", 1)]
    [TestCase("Second Post", "Body of second post", 2)]
    [TestCase("Third Post", "Body of third post", 3)]
    [Description("Verify POST /posts works with various data combinations")]
    public async Task CreatePost_DataDriven_Returns201(string title, string body, int userId)
    {
        // Arrange
        var newPost = new CreatePostRequest { Title = title, Body = body, UserId = userId };

        // Act
        var response = await _postService.CreatePostAsync(newPost);

        // Assert
        AssertStatusCode(response, 201);

        var result = await DeserializeResponseAsync<PostResponse>(response);
        result.Title.Should().Be(title);
        result.Body.Should().Be(body);
        result.UserId.Should().Be(userId);
    }
}
