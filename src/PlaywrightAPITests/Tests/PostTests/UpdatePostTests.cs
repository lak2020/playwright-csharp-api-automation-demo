using Allure.Net.Commons;

namespace PlaywrightAPITests.Tests.PostTests;

/// <summary>
/// Tests for PUT and PATCH /posts/{id} endpoints.
/// Covers: full update, partial update, and response validation.
/// </summary>
[TestFixture]
[Category("Posts")]
[Category("PUT")]
[Category("PATCH")]
[AllureSuite("Post Management")]
[AllureSubSuite("Update Posts")]
public class UpdatePostTests : BaseApiTest
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
    [Description("Verify PUT /posts/{id} fully updates a post and returns 200")]
    public async Task UpdatePost_WithPut_Returns200()
    {
        // Arrange
        const int postId = 1;
        var updateData = new UpdatePostRequest
        {
            Title = "Updated Title",
            Body = "Updated body content",
            UserId = 1
        };

        // Act
        var response = await _postService.UpdatePostAsync(postId, updateData);

        // Assert
        AssertStatusCode(response, 200);

        var result = await DeserializeResponseAsync<PostResponse>(response);
        result.Id.Should().Be(postId);
        result.Title.Should().Be(updateData.Title);
        result.Body.Should().Be(updateData.Body);
        result.UserId.Should().Be(updateData.UserId);
    }

    [Test]
    [AllureTag("Smoke")]
    [Description("Verify PATCH /posts/{id} partially updates a post")]
    public async Task UpdatePost_WithPatch_Returns200()
    {
        // Arrange
        const int postId = 1;
        var patchData = new { title = "Patched Title Only" };

        // Act
        var response = await _postService.PatchPostAsync(postId, patchData);

        // Assert
        AssertStatusCode(response, 200);

        var result = await DeserializeResponseAsync<PostResponse>(response);
        result.Id.Should().Be(postId);
        result.Title.Should().Be("Patched Title Only");
        // Body and userId should still exist (not cleared)
        result.UserId.Should().BeGreaterThan(0);
    }

    [Test]
    [Description("Verify PUT /posts/{id} with random data updates correctly")]
    public async Task UpdatePost_WithRandomData_Returns200()
    {
        // Arrange
        const int postId = 5;
        var updateData = TestDataGenerator.GenerateUpdatePostRequest();

        // Act
        var response = await _postService.UpdatePostAsync(postId, updateData);

        // Assert
        AssertStatusCode(response, 200);

        var result = await DeserializeResponseAsync<PostResponse>(response);
        result.Id.Should().Be(postId);
        result.Title.Should().Be(updateData.Title);
        result.Body.Should().Be(updateData.Body);
    }
}
