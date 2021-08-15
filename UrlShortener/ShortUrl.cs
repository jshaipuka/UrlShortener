using System;
using System.ComponentModel.DataAnnotations;
using static System.ComponentModel.DataAnnotations.DataType;

namespace UrlShortener
{
    public class ShortUrl
    {
        public string? Alias { get; set; }

        [Required] [DataType(Url)] public string OriginalUrl { get; set; }

        public DateTime? ExpiresAt { get; set; }
    }
}