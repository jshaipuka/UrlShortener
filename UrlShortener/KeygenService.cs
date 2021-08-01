using System;
using System.Threading.Tasks;

namespace UrlShortener
{
    public class KeygenService : IKeygenService
    {
        public async Task<string> AllocateKey()
        {
            // TODO: write code.
            return "key from service";
        }

        public async Task ReleaseKey(string key)
        {
            try
            {
                // TODO: write code.
            }
            catch (Exception e)
            {
                Console.WriteLine("Doesn't matter");
                throw;
            }
        }
    }
}