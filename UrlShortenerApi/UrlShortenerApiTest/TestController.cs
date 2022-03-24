using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UrlShortenerApi.Models;

namespace UrlShortenerApiTest;

public class TestController
{
    private HttpClient _api;

    [SetUp]
    public async Task Setup()
    {
        var app = new TestApi();
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var db = provider.GetRequiredService<ShortUrlDb>())
            {
                await db.Database.EnsureCreatedAsync();
                await db.AddRangeAsync(MockData.GetUrls());
                await db.SaveChangesAsync();
            }
        }
        _api = app.CreateClient();
    }

    [Test]
    public async Task ShouldGetShortUrlWhenValidHashProvided()
    {
        var url = await _api.GetStringAsync("/url/test1");

        Assert.IsNotNull(url);
    }
}