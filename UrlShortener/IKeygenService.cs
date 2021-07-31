using System.Threading.Tasks;

namespace UrlShortener
{
    public interface IKeygenService
    {
        Task<string> AllocateKey();
        Task ReleaseKey(string key);
    }
}