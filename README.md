# 🎭 Playwright C# API Automation Demo

A modern API test automation framework built with **Playwright for .NET**, **NUnit 4**, and **C# 12** targeting **.NET 8**.

Demonstrates best practices for API testing including service object pattern, fluent assertions, structured logging, Allure reporting, and CI/CD integration.

---

### 🔗 Quick Links

| Resource | Link |
|----------|------|
| 📊 **Test Report** | [Allure Report on GitHub Pages](https://lak2020.github.io/playwright-csharp-api-automation-demo) |
| ⚙️ **CI/CD Pipeline** | [GitHub Actions Runs](https://github.com/lak2020/playwright-csharp-api-automation-demo/actions) |
| 💻 **Source Code** | [GitHub Repository](https://github.com/lak2020/playwright-csharp-api-automation-demo) |

---

## 🏗️ Project Structure

```
src/PlaywrightAPITests/
├── Base/                       # Base test class & API client factory
│   ├── ApiClientFactory.cs     # Playwright API context management
│   └── BaseApiTest.cs          # Shared setup/teardown, logging, helpers
├── Config/                     # Configuration management
│   └── TestConfiguration.cs    # Centralized config via appsettings.json
├── Models/                     # Request/Response DTOs
│   ├── Request/                # CreatePostRequest, UpdatePostRequest
│   └── Response/               # PostResponse, UserResponse, etc.
├── Services/                   # Service Object Pattern (API abstraction)
│   ├── PostApiService.cs       # Post CRUD operations
│   ├── UserApiService.cs       # User read operations
│   └── TodoApiService.cs       # Todo read operations
├── Tests/                      # Test classes organized by feature
│   ├── PostTests/              # GET, POST, PUT, PATCH, DELETE posts
│   ├── UserTests/              # GET users
│   └── TodoTests/              # GET todos
├── Utilities/                  # Helpers & extensions
│   ├── JsonHelper.cs           # JSON serialization utilities
│   ├── TestDataGenerator.cs    # Random test data via Bogus
│   └── AssertionExtensions.cs  # Custom fluent assertion extensions
├── TestData/                   # Static test data files
├── appsettings.json            # Test configuration
└── GlobalUsings.cs             # Global using directives
```

## 🚀 Tech Stack

| Technology | Purpose |
|---|---|
| **.NET 8** | Runtime & SDK |
| **Playwright** | HTTP client for API testing |
| **NUnit 4** | Test framework |
| **FluentAssertions 7** | Readable assertions |
| **Bogus** | Fake test data generation |
| **Serilog** | Structured logging (console + file) |
| **Allure** | Test reporting |

## 📋 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- PowerShell (for Playwright browser install script)

## ⚡ Quick Start

```bash
# 1. Clone the repository
git clone https://github.com/lak2020/playwright-csharp-api-automation-demo.git
cd playwright-csharp-api-automation-demo

# 2. Restore NuGet packages
dotnet restore

# 3. Build the project
dotnet build

# 4. Install Playwright dependencies
pwsh src/PlaywrightAPITests/bin/Debug/net8.0/playwright.ps1 install

# 5. Run all tests
dotnet test

# 6. Run tests with detailed output
dotnet test --logger "console;verbosity=detailed"
```

## 🧪 Running Tests

```bash
# Run all tests
dotnet test

# Run specific test category
dotnet test --filter "Category=Smoke"
dotnet test --filter "Category=Posts"
dotnet test --filter "Category=Users"
dotnet test --filter "Category=Todos"
dotnet test --filter "Category=Negative"

# Run a specific test class
dotnet test --filter "FullyQualifiedName~CreatePostTests"

# Run with TRX result file
dotnet test --logger "trx;LogFileName=results.trx" --results-directory TestResults
```

## 📊 Test API (JSONPlaceholder)

This project uses [JSONPlaceholder](https://jsonplaceholder.typicode.com) — a free, no-auth-required fake REST API:

| Endpoint | Method | Description |
|---|---|---|
| `/posts` | GET | List all posts (100 items) |
| `/posts/{id}` | GET | Single post |
| `/posts?userId={id}` | GET | Posts by user |
| `/posts/{id}/comments` | GET | Comments for a post |
| `/posts` | POST | Create post |
| `/posts/{id}` | PUT | Update post (full) |
| `/posts/{id}` | PATCH | Update post (partial) |
| `/posts/{id}` | DELETE | Delete post |
| `/users` | GET | List all users (10 items) |
| `/users/{id}` | GET | Single user |
| `/todos` | GET | List all todos (200 items) |
| `/todos/{id}` | GET | Single todo |

## 🔧 Configuration

Edit `src/PlaywrightAPITests/appsettings.json` to customize:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://jsonplaceholder.typicode.com",
    "Timeout": 30000,
    "RetryCount": 2
  }
}
```

Override via environment variables: `ApiSettings__BaseUrl=https://your-api.com`

## 📝 Key Design Patterns

- **Service Object Pattern** — API operations encapsulated in service classes
- **Base Test Class** — Shared lifecycle management, logging, and assertion helpers
- **Factory Pattern** — `ApiClientFactory` manages Playwright context creation
- **Configuration Pattern** — Environment-aware config via `IConfiguration`
- **Data-Driven Testing** — `[TestCase]` attributes + Bogus fake data

## 📊 Allure Reporting

```bash
# After running tests, generate and view the report:
allure serve allure-results
```

## 🔄 CI/CD

GitHub Actions workflow runs on push to `main`/`develop` and on PRs. See `.github/workflows/api-tests.yml`.

## 📜 License

MIT
