using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace UrlShortener
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly Container _container;

        public ShortUrlRepository(Container container)
        {
            this._container = container;
        }

        public async Task<ShortUrlEntity> GetById(string id)
        {
            try
            {
                ItemResponse<ShortUrlEntity> response = await this._container.ReadItemAsync<ShortUrlEntity>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch(CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            { 
                return null;
            }
        }

        public async Task Create(ShortUrlEntity shortUrl)
        {
            await this._container.CreateItemAsync<ShortUrlEntity>(shortUrl, new PartitionKey(shortUrl.Id));
        }

        public async Task DeleteById(string id)
        {
            await this._container.DeleteItemAsync<ShortUrlEntity>(id, new PartitionKey(id));
        }
    }
}