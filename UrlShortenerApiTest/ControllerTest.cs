using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UrlShortenerApi.Models;
using FluentAssertions;

namespace UrlShortenerApiTest;

public class ControllerTest
{
    private HttpClient _api;

    [SetUp]
    public async Task Setup()
    {
        var app = new TestApi();
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            await using (var db = provider.GetRequiredService<ShortUrlDb>())
            {
                await db.Database.EnsureCreatedAsync();
                await db.AddRangeAsync(MockData.GetUrls());
                await db.SaveChangesAsync();
            }
        }
        _api = app.CreateClient();
    }

    [Test]
    public async Task Should_Get_ShortUrl_When_Valid_Hash_Provided()
    {
        var response = await _api.GetAsync("/url/test1");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ShortUrl>();
        result.Should().BeOfType<ShortUrl>().Which.UrlHash.Should().Be("test1");
    }
    
    [Test]
    public async Task Should_Return_NotFound_When_Non_Existing_Hash_Provided()
    {
        var response = await _api.GetAsync("/url/invalid");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}