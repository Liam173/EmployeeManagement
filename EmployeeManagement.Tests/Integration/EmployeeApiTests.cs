using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace EmployeeManagement.Tests.Integration;

public class EmployeeApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public EmployeeApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetEmployee_ReturnsUnauthorized_WhenNoTokenProvided()
    {
        // Act
        var response = await _client.GetAsync(
            "/api/v1/Employee/GetEmployee/1");

        // Assert
        Assert.Equal(
            HttpStatusCode.Unauthorized,
            response.StatusCode);
    }

    [Fact]
    public async Task GetInstanceId_ReturnsSameSingletonGuid()
    {
        // Act
        var response = await _client.GetAsync(
            "/api/v1/Employee/InstanceId");

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<InstanceIdResponse>();

        // Assert
        Assert.NotNull(result);

        Assert.Equal(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            result.First);

        Assert.Equal(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            result.Second);
    }
}