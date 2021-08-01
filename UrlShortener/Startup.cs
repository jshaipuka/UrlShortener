using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace UrlShortener
{
    public class Startup
    {
        public IConfiguration Config { get; }

        public Startup(IConfiguration configuration)
        {
            this.Config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>c.SwaggerDoc("v1", new OpenApiInfo {Title = "UrlShortener", Version = "v1"}));
            services.AddCors(options => options.AddPolicy("cors:AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddSingleton<IShortUrlRepository>(InitializeShortUrlRepository().GetAwaiter().GetResult());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrlShortener v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        private async Task<ShortUrlRepository> InitializeShortUrlRepository()
        {
            Container container = await InitializeCosmosDbContainer();
            ShortUrlRepository shortUrlRepository = new ShortUrlRepository(container);
            return shortUrlRepository;
        }

        private async Task<Container> InitializeCosmosDbContainer()
        {
            var dbConfig = Config.GetSection("CosmosDb");

            string account = dbConfig.GetSection("Account").Value;
            string key = dbConfig.GetSection("Key").Value;
            CosmosClient client = new CosmosClient(account, key);
            
            string databaseName = dbConfig.GetSection("DatabaseName").Value;
            string containerName = dbConfig.GetSection("ContainerName").Value;
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            
            return client.GetContainer(databaseName, containerName);
        }
    }
}