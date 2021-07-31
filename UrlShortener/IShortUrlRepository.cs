using System.Threading.Tasks;

namespace UrlShortener
{
    public interface IShortUrlRepository
    {
        Task<ShortUrlEntity?> GetById(string id);
        Task Create(ShortUrlEntity shortUrl);
        Task DeleteById(string id);
    }
}