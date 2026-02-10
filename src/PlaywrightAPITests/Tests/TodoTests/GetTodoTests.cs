using Allure.Net.Commons;

namespace PlaywrightAPITests.Tests.TodoTests;

/// <summary>
/// Tests for GET /todos and GET /todos/{id} endpoints.
/// Covers: list todos, single todo, filter by user, and not found.
/// </summary>
[TestFixture]
[Category("Todos")]
[Category("GET")]
[AllureSuite("Todo Management")]
[AllureSubSuite("Get Todos")]
public class GetTodoTests : BaseApiTest
{
    private TodoApiService _todoService = null!;

    [OneTimeSetUp]
    public override async Task OneTimeSetUp()
    {
        await base.OneTimeSetUp();
        _todoService = new TodoApiService(Request);
    }

    [Test]
    [AllureTag("Smoke")]
    [Description("Verify GET /todos returns list of 200 todos")]
    public async Task GetTodos_ReturnsList_With200Todos()
    {
        // Act
        var response = await _todoService.GetTodosAsync();

        // Assert
        AssertStatusCode(response, 200);

        var todos = await DeserializeResponseAsync<List<TodoResponse>>(response);
        todos.Should().HaveCount(200);
        todos.Should().AllSatisfy(todo =>
        {
            todo.Id.Should().BeGreaterThan(0);
            todo.UserId.Should().BeGreaterThan(0);
            todo.Title.Should().NotBeNullOrEmpty();
        });
    }

    [Test]
    [AllureTag("Smoke")]
    [Description("Verify GET /todos/1 returns a single todo")]
    public async Task GetTodoById_ReturnsCorrectTodo()
    {
        // Arrange
        const int todoId = 1;

        // Act
        var response = await _todoService.GetTodoByIdAsync(todoId);

        // Assert
        AssertStatusCode(response, 200);

        var todo = await DeserializeResponseAsync<TodoResponse>(response);
        todo.Id.Should().Be(todoId);
        todo.UserId.Should().BeGreaterThan(0);
        todo.Title.Should().NotBeNullOrEmpty();
    }

    [Test]
    [Description("Verify GET /todos?userId=1 returns only todos for user 1")]
    public async Task GetTodosByUserId_ReturnsFilteredTodos()
    {
        // Arrange
        const int userId = 1;

        // Act
        var response = await _todoService.GetTodosByUserIdAsync(userId);

        // Assert
        AssertStatusCode(response, 200);

        var todos = await DeserializeResponseAsync<List<TodoResponse>>(response);
        todos.Should().NotBeEmpty();
        todos.Should().AllSatisfy(todo =>
        {
            todo.UserId.Should().Be(userId);
        });
    }

    [Test]
    [AllureTag("Negative")]
    [Description("Verify GET /todos/999 returns 404 for non-existent todo")]
    public async Task GetTodoById_NonExistent_Returns404()
    {
        // Act
        var response = await _todoService.GetTodoByIdAsync(999);

        // Assert
        AssertStatusCode(response, 404);
    }

    [Test]
    [Description("Verify todos contain both completed and incomplete items")]
    public async Task GetTodos_ContainsBothCompletedAndIncomplete()
    {
        // Act
        var response = await _todoService.GetTodosAsync();

        // Assert
        AssertStatusCode(response, 200);

        var todos = await DeserializeResponseAsync<List<TodoResponse>>(response);
        todos.Should().Contain(t => t.Completed == true);
        todos.Should().Contain(t => t.Completed == false);
    }
}
