using System.Net.Http.Json;
using API.Models;
using FluentAssertions;
using Xunit;

namespace BooksAPI.IntegrationTests;

public class DepartmentsApiTests
{
    private readonly ApiTestBase<Department> _apiTestBase;

    public DepartmentsApiTests(ApiTestBase<Department> apiTestBase)
    {
        _apiTestBase = apiTestBase;
    }

    [Fact]
    public async Task GetDepartments_ShouldReturnAllDepartments()
    {
        var client = _apiTestBase.GetClient;
        var response = await client.GetAsync(Routes.Departments);
        response.Should().BeSuccessful();
        var departments = await response.Content.ReadFromJsonAsync<List<Department>>();
        departments.Should().HaveCount(3);
    }
}