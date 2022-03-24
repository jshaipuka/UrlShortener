using UrlShortenerApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app
    .MapGet("/url/{urlHash}", (string urlHash) =>
    {
        var url = new ShortUrl() { UrlHash = urlHash, OriginalUrl = "https://google.com" };
        return url.OriginalUrl;
    })
    .Produces<string>(StatusCodes.Status200OK)
    .WithName("GetOriginalUrl")
    .WithTags("Getters");

app.Run();