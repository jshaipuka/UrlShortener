using System;
using System.ComponentModel.DataAnnotations;
using static System.ComponentModel.DataAnnotations.DataType;

namespace UrlShortener
{
    public class ShortUrlEntity
    {
        public string Id { get; set; }

        public string OriginalUrl { get; set; }

        public DateTime? ExpiresAt { get; set; }
    }
}