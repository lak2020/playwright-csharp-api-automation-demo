using Allure.Net.Commons;

namespace PlaywrightAPITests.Tests.UserTests;

/// <summary>
/// Tests for GET /users and GET /users/{id} endpoints.
/// Covers: list users, single user, user data structure, and not found.
/// </summary>
[TestFixture]
[Category("Users")]
[Category("GET")]
[AllureSuite("User Management")]
[AllureSubSuite("Get Users")]
public class GetUserTests : BaseApiTest
{
    private UserApiService _userService = null!;

    [OneTimeSetUp]
    public override async Task OneTimeSetUp()
    {
        await base.OneTimeSetUp();
        _userService = new UserApiService(Request);
    }

    [Test]
    [AllureTag("Smoke")]
    [Description("Verify GET /users returns list of 10 users")]
    public async Task GetUsers_ReturnsList_With10Users()
    {
        // Act
        var response = await _userService.GetUsersAsync();

        // Assert
        AssertStatusCode(response, 200);

        var users = await DeserializeResponseAsync<List<UserResponse>>(response);
        users.Should().HaveCount(10);
        users.Should().AllSatisfy(user =>
        {
            user.Id.Should().BeGreaterThan(0);
            user.Name.Should().NotBeNullOrEmpty();
            user.Username.Should().NotBeNullOrEmpty();
            user.Email.Should().NotBeNullOrEmpty();
        });
    }

    [Test]
    [AllureTag("Smoke")]
    [Description("Verify GET /users/1 returns user with complete data structure")]
    public async Task GetUserById_ReturnsCompleteUser()
    {
        // Arrange
        const int userId = 1;

        // Act
        var response = await _userService.GetUserByIdAsync(userId);

        // Assert
        AssertStatusCode(response, 200);

        var user = await DeserializeResponseAsync<UserResponse>(response);
        user.Id.Should().Be(userId);
        user.Name.Should().NotBeNullOrEmpty();
        user.Username.Should().NotBeNullOrEmpty();
        user.Email.Should().NotBeNullOrEmpty();
        user.Phone.Should().NotBeNullOrEmpty();
        user.Website.Should().NotBeNullOrEmpty();
        user.Address.Should().NotBeNull();
        user.Company.Should().NotBeNull();
    }

    [Test]
    [AllureTag("Negative")]
    [Description("Verify GET /users/999 returns 404 for non-existent user")]
    public async Task GetUserById_NonExistentUser_Returns404()
    {
        // Act
        var response = await _userService.GetUserByIdAsync(999);

        // Assert
        AssertStatusCode(response, 404);
    }

    [Test]
    [Description("Verify GET /users/{id} returns correct user for various IDs")]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public async Task GetUserById_VariousIds_ReturnsMatchingUser(int userId)
    {
        // Act
        var response = await _userService.GetUserByIdAsync(userId);

        // Assert
        AssertStatusCode(response, 200);

        var user = await DeserializeResponseAsync<UserResponse>(response);
        user.Id.Should().Be(userId);
    }
}
