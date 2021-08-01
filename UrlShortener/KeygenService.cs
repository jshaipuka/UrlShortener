using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace UrlShortener
{
    public class KeygenService
    {
        private readonly HttpClient _client;

        public KeygenService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> AllocateKey() => (await _client.GetFromJsonAsync<IList<string>>("/api/v1/keys?limit=1"))!.First();

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