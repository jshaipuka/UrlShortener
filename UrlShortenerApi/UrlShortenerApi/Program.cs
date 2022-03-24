using UrlShortenerApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/url/{urlHash}", (string urlHash) =>
{
    var url = new ShortUrl() { UrlHash = urlHash, OriginalUrl = "https://google.com" };
    return url.OriginalUrl;
});

app.Run();