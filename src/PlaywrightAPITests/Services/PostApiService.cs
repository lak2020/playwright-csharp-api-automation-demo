using Serilog;

namespace PlaywrightAPITests.Services;

/// <summary>
/// Service layer encapsulating all Post API operations (JSONPlaceholder /posts).
/// Follows the Service Object Pattern for clean test code.
/// </summary>
public class PostApiService
{
    private readonly IAPIRequestContext _request;

    public PostApiService(IAPIRequestContext request)
    {
        _request = request;
    }

    /// <summary>
    /// GET /posts — Retrieve all posts.
    /// </summary>
    public async Task<IAPIResponse> GetPostsAsync()
    {
        Log.Information("GET /posts");
        return await _request.GetAsync("/posts");
    }

    /// <summary>
    /// GET /posts/{id} — Retrieve a single post by ID.
    /// </summary>
    public async Task<IAPIResponse> GetPostByIdAsync(int id)
    {
        Log.Information("GET /posts/{Id}", id);
        return await _request.GetAsync($"/posts/{id}");
    }

    /// <summary>
    /// GET /posts?userId={userId} — Retrieve posts filtered by user.
    /// </summary>
    public async Task<IAPIResponse> GetPostsByUserIdAsync(int userId)
    {
        Log.Information("GET /posts?userId={UserId}", userId);
        return await _request.GetAsync($"/posts?userId={userId}");
    }

    /// <summary>
    /// GET /posts/{id}/comments — Retrieve comments for a specific post.
    /// </summary>
    public async Task<IAPIResponse> GetPostCommentsAsync(int postId)
    {
        Log.Information("GET /posts/{PostId}/comments", postId);
        return await _request.GetAsync($"/posts/{postId}/comments");
    }

    /// <summary>
    /// POST /posts — Create a new post.
    /// </summary>
    public async Task<IAPIResponse> CreatePostAsync(CreatePostRequest post)
    {
        Log.Information("POST /posts — Title: {Title}, UserId: {UserId}", post.Title, post.UserId);
        return await _request.PostAsync("/posts", new()
        {
            DataObject = post
        });
    }

    /// <summary>
    /// PUT /posts/{id} — Full update of a post.
    /// </summary>
    public async Task<IAPIResponse> UpdatePostAsync(int id, UpdatePostRequest post)
    {
        Log.Information("PUT /posts/{Id} — Title: {Title}", id, post.Title);
        return await _request.PutAsync($"/posts/{id}", new()
        {
            DataObject = post
        });
    }

    /// <summary>
    /// PATCH /posts/{id} — Partial update of a post.
    /// </summary>
    public async Task<IAPIResponse> PatchPostAsync(int id, object patchData)
    {
        Log.Information("PATCH /posts/{Id}", id);
        return await _request.PatchAsync($"/posts/{id}", new()
        {
            DataObject = patchData
        });
    }

    /// <summary>
    /// DELETE /posts/{id} — Delete a post.
    /// </summary>
    public async Task<IAPIResponse> DeletePostAsync(int id)
    {
        Log.Information("DELETE /posts/{Id}", id);
        return await _request.DeleteAsync($"/posts/{id}");
    }
}
