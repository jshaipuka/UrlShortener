using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using UrlShortenerApi.Models;

namespace UrlShortenerApiTest;

class TestApi: WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ShortUrlDb>));
            services.AddDbContext<ShortUrlDb>(options =>
                options.UseInMemoryDatabase("TestDB", root));
        });

        return base.CreateHost(builder);
    }
}