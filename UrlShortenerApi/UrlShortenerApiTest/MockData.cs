using System.Collections.Generic;
using System.Linq;
using UrlShortenerApi.Models;

namespace UrlShortenerApiTest;

public class MockData
{
    public static IEnumerable<ShortUrl> GetUrls() => Enumerable.Empty<ShortUrl>()
        .Append(new ShortUrl() { UrlHash = "test1", OriginalUrl = "https://google.com"})
        .Append(new ShortUrl() { UrlHash = "test2", OriginalUrl = "https://www.google.com"});
}