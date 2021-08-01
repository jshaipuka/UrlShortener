using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace UrlShortener
{
    public class KeygenService : IKeygenService
    {
        public HttpClient Client { get; }

        public KeygenService(HttpClient client)
        {
            Client = client;
        }

        public async Task<string> AllocateKey() => (await Client.GetFromJsonAsync<IList<string>>("/keys?limit=5"))!.First();

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