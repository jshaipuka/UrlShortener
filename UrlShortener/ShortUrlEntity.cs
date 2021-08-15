using System;
using Newtonsoft.Json;

namespace UrlShortener
{
    public class ShortUrlEntity
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("originalUrl")] public string OriginalUrl { get; set; }

        [JsonProperty("expiresAt")] public DateTime ExpiresAt { get; set; }
    }
}