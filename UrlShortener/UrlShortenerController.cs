using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener
{
    [ApiController]
    [Route("api/v1")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly KeygenService _keygenService;
        private readonly IShortUrlRepository _repository;

        public UrlShortenerController(IShortUrlRepository repository, KeygenService keygenService)
        {
            _repository = repository;
            _keygenService = keygenService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOriginalUrl(string? id)
        {
            if (id == null) return BadRequest("Please provide shortened URL.");
            var shortUrl = await _repository.GetById(id);

            if (shortUrl == null) return NotFound();
            if (shortUrl.ExpiresAt >= DateTime.UtcNow)
            {
                await _keygenService.ReleaseKey(id);
                return NotFound();
            }

            return Ok(shortUrl.OriginalUrl);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUrl([FromBody] ShortUrl shortUrl)
        {
            var allocatedKey = await GetAllocatedKey(shortUrl.Alias);
            if (string.IsNullOrEmpty(allocatedKey)) return BadRequest("Cannot create short URL. Key is taken.");

            var newShortUrl = new ShortUrlEntity
            {
                Id = allocatedKey, OriginalUrl = shortUrl.OriginalUrl,
                ExpiresAt = shortUrl.ExpiresAt ?? DateTime.UtcNow.AddYears(2)
            };
            await _repository.Create(newShortUrl);
            return Ok(newShortUrl);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrl(string? id)
        {
            if (id == null) return BadRequest("Please provide shortened URL.");

            var shortUrl = await _repository.GetById(id);
            if (shortUrl == null) return NotFound();

            await _repository.DeleteById(id);
            await _keygenService.ReleaseKey(id);
            return Ok();
        }

        private async Task<string> GetAllocatedKey(string? alias)
        {
            if (string.IsNullOrWhiteSpace(alias)) return await _keygenService.AllocateKey();
            return await _repository.GetById(alias) == null ? alias : string.Empty;
        }
    }
}