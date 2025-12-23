using FluentAssertions;
using SchoolManagement.Api.IntegrationTests.Common;
using System.Net;

namespace SchoolManagement.Api.IntegrationTests.Admin;

public class AdminAuthorizationTests
    : IClassFixture<TestApplicationFactory>
{
    private readonly HttpClient _client;

    public AdminAuthorizationTests(TestApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Non_Admin_Cannot_Access_Admin_Endpoint()
    {
        // Act
        var response = await _client.GetAsync("/api/admin/users");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}