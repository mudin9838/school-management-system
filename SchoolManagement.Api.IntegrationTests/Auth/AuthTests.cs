using FluentAssertions;
using SchoolManagement.Api.IntegrationTests.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace SchoolManagement.Api.IntegrationTests.Auth;

public class AuthTests : IClassFixture<TestApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthTests(TestApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_Then_Login_Returns_Jwt()
    {
        try
        {
            var register = new
            {
                Email = "admin@test.com",
                Password = "Admin123!",
                Roles = new List<string> { "Admin" }
            };

            Console.WriteLine($"Sending registration request for: {register.Email}");

            // First, try to register
            var registerResponse = await _client.PostAsJsonAsync("/api/auth/register", register);

            // Get the actual error message
            if (!registerResponse.IsSuccessStatusCode)
            {
                var errorContent = await registerResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Register failed with status {registerResponse.StatusCode}: {errorContent}");

                registerResponse.EnsureSuccessStatusCode();
            }
            else
            {
                Console.WriteLine("Registration successful!");
            }

            // Small delay to ensure user is committed
            await Task.Delay(100);

            var login = new
            {
                Email = "admin@test.com",
                Password = "Admin123!"
            };

            Console.WriteLine($"Sending login request for: {login.Email}");

            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", login);

            if (!loginResponse.IsSuccessStatusCode)
            {
                var errorContent = await loginResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Login failed with status {loginResponse.StatusCode}: {errorContent}");

                // Try to debug: Check if user exists in database
                await DebugUserExistence();

                loginResponse.EnsureSuccessStatusCode();
            }
            else
            {
                Console.WriteLine("Login successful!");
            }

            var result = await loginResponse.Content.ReadFromJsonAsync<JsonElement>();
            result.GetProperty("accessToken").GetString()
                .Should().NotBeNullOrEmpty();

            Console.WriteLine("Test completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed with exception: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    private async Task DebugUserExistence()
    {
        Console.WriteLine("Debug: Checking user existence...");
    }
}

