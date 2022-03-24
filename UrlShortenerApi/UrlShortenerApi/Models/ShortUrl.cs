using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UrlShortenerApi.Models;

public class ShortUrl
{
    [Key]
    public string UrlHash { get; set; }

    public string OriginalUrl { get; set; }
}

class ShortUrlDb : DbContext
{
    public ShortUrlDb(DbContextOptions<ShortUrlDb> options) : base(options) {}
    public DbSet<ShortUrl> ShortUrls => Set<ShortUrl>();
}