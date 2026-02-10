using Allure.Net.Commons;

namespace PlaywrightAPITests.Tests.PostTests;

/// <summary>
/// Tests for GET /posts and GET /posts/{id} endpoints.
/// Covers: list posts, single post, filter by user, nested comments, and not found.
/// </summary>
[TestFixture]
[Category("Posts")]
[Category("GET")]
[AllureSuite("Post Management")]
[AllureSubSuite("Get Posts")]
public class GetPostTests : BaseApiTest
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
    [Description("Verify GET /posts returns list of 100 posts")]
    public async Task GetPosts_ReturnsList_With100Posts()
    {
        // Act
        var response = await _postService.GetPostsAsync();

        // Assert
        AssertStatusCode(response, 200);

        var posts = await DeserializeResponseAsync<List<PostResponse>>(response);
        posts.Should().HaveCount(100);
        posts.Should().AllSatisfy(post =>
        {
            post.Id.Should().BeGreaterThan(0);
            post.UserId.Should().BeGreaterThan(0);
            post.Title.Should().NotBeNullOrEmpty();
            post.Body.Should().NotBeNullOrEmpty();
        });
    }

    [Test]
    [AllureTag("Smoke")]
    [Description("Verify GET /posts/1 returns a single post with correct data")]
    public async Task GetPostById_ReturnsCorrectPost()
    {
        // Arrange
        const int postId = 1;

        // Act
        var response = await _postService.GetPostByIdAsync(postId);

        // Assert
        AssertStatusCode(response, 200);

        var post = await DeserializeResponseAsync<PostResponse>(response);
        post.Id.Should().Be(postId);
        post.UserId.Should().BeGreaterThan(0);
        post.Title.Should().NotBeNullOrEmpty();
        post.Body.Should().NotBeNullOrEmpty();
    }

    [Test]
    [AllureTag("Negative")]
    [Description("Verify GET /posts/999 returns 404 for non-existent post")]
    public async Task GetPostById_NonExistentPost_Returns404()
    {
        // Act
        var response = await _postService.GetPostByIdAsync(999);

        // Assert
        AssertStatusCode(response, 404);
    }

    [Test]
    [Description("Verify GET /posts?userId=1 returns only posts for user 1")]
    public async Task GetPostsByUserId_ReturnsFilteredPosts()
    {
        // Arrange
        const int userId = 1;

        // Act
        var response = await _postService.GetPostsByUserIdAsync(userId);

        // Assert
        AssertStatusCode(response, 200);

        var posts = await DeserializeResponseAsync<List<PostResponse>>(response);
        posts.Should().NotBeEmpty();
        posts.Should().AllSatisfy(post =>
        {
            post.UserId.Should().Be(userId);
        });
    }

    [Test]
    [Description("Verify GET /posts/1/comments returns comments for post 1")]
    public async Task GetPostComments_ReturnsCommentsList()
    {
        // Arrange
        const int postId = 1;

        // Act
        var response = await _postService.GetPostCommentsAsync(postId);

        // Assert
        AssertStatusCode(response, 200);

        var comments = await DeserializeResponseAsync<List<CommentResponse>>(response);
        comments.Should().NotBeEmpty();
        comments.Should().AllSatisfy(comment =>
        {
            comment.PostId.Should().Be(postId);
            comment.Id.Should().BeGreaterThan(0);
            comment.Name.Should().NotBeNullOrEmpty();
            comment.Email.Should().NotBeNullOrEmpty();
            comment.Body.Should().NotBeNullOrEmpty();
        });
    }
}
