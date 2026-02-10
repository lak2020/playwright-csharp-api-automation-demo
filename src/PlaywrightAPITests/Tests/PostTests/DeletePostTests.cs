using Allure.Net.Commons;

namespace PlaywrightAPITests.Tests.PostTests;

/// <summary>
/// Tests for DELETE /posts/{id} endpoint.
/// </summary>
[TestFixture]
[Category("Posts")]
[Category("DELETE")]
[AllureSuite("Post Management")]
[AllureSubSuite("Delete Posts")]
public class DeletePostTests : BaseApiTest
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
    [Description("Verify DELETE /posts/{id} deletes a post and returns 200")]
    public async Task DeletePost_ExistingPost_Returns200()
    {
        // Arrange
        const int postId = 1;

        // Act
        var response = await _postService.DeletePostAsync(postId);

        // Assert
        AssertStatusCode(response, 200);
    }

    [Test]
    [Description("Verify DELETE /posts/{id} for various IDs returns 200")]
    [TestCase(1)]
    [TestCase(50)]
    [TestCase(100)]
    public async Task DeletePost_VariousIds_Returns200(int postId)
    {
        // Act
        var response = await _postService.DeletePostAsync(postId);

        // Assert
        AssertStatusCode(response, 200);
    }
}
