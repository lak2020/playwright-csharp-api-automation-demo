using Serilog;

namespace PlaywrightAPITests.Services;

/// <summary>
/// Service layer encapsulating User API operations (JSONPlaceholder /users).
/// </summary>
public class UserApiService
{
    private readonly IAPIRequestContext _request;

    public UserApiService(IAPIRequestContext request)
    {
        _request = request;
    }

    /// <summary>
    /// GET /users — Retrieve all users.
    /// </summary>
    public async Task<IAPIResponse> GetUsersAsync()
    {
        Log.Information("GET /users");
        return await _request.GetAsync("/users");
    }

    /// <summary>
    /// GET /users/{id} — Retrieve a single user by ID.
    /// </summary>
    public async Task<IAPIResponse> GetUserByIdAsync(int id)
    {
        Log.Information("GET /users/{Id}", id);
        return await _request.GetAsync($"/users/{id}");
    }
}
