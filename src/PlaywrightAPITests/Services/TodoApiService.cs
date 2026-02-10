using Serilog;

namespace PlaywrightAPITests.Services;

/// <summary>
/// Service layer for Todo API operations (JSONPlaceholder /todos).
/// </summary>
public class TodoApiService
{
    private readonly IAPIRequestContext _request;

    public TodoApiService(IAPIRequestContext request)
    {
        _request = request;
    }

    /// <summary>
    /// GET /todos — Retrieve all todos.
    /// </summary>
    public async Task<IAPIResponse> GetTodosAsync()
    {
        Log.Information("GET /todos");
        return await _request.GetAsync("/todos");
    }

    /// <summary>
    /// GET /todos/{id} — Retrieve a single todo by ID.
    /// </summary>
    public async Task<IAPIResponse> GetTodoByIdAsync(int id)
    {
        Log.Information("GET /todos/{Id}", id);
        return await _request.GetAsync($"/todos/{id}");
    }

    /// <summary>
    /// GET /todos?userId={userId} — Retrieve todos filtered by user.
    /// </summary>
    public async Task<IAPIResponse> GetTodosByUserIdAsync(int userId)
    {
        Log.Information("GET /todos?userId={UserId}", userId);
        return await _request.GetAsync($"/todos?userId={userId}");
    }
}
