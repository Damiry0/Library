using System.Net;
using System.Text;
using API.Models;
using BooksAPI.Command;
using FluentAssertions;
using Newtonsoft.Json;

namespace BooksAPI.IntegrationTests;

public class BooksEntityApiTests
{
    [Fact]
    public async Task GivenCreatingBookEntity_WithValidValues_ShouldReturnNoContent()
    {
        var client = new HttpClient();

        var command = new CreateBookCommand("Title", DateTime.Today, "Isbn", 100, 10, "Description", new List<Author>(),
            new List<Edition>());

        var response = await client.PostAsync(Routes.Books,
            new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"));
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}