using System;
using System.Threading.Tasks;

namespace UrlShortener
{
    public class KeygenService : IKeygenService
    {
        public Task<string> AllocateKey()
        {
            throw new System.NotImplementedException();
        }

        public Task ReleaseKey(string key)
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