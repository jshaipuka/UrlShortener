using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShortUrlDb>(options => {
    options.UseInMemoryDatabase(databaseName: "MyLocalDb"); 
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app
    .MapGet("/url/{urlHash}", async (ShortUrlDb db, string urlHash) =>
        await db.ShortUrls.SingleOrDefaultAsync(su => su.UrlHash == urlHash) is ShortUrl url
        ? Results.Ok(url)
        : Results.NotFound())
    .Produces<string>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetOriginalUrl")
    .WithTags("Getters");

app.Run();