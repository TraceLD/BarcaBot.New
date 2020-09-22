using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Settings;
using BarcaBot.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BarcaBot.Infrastructure.Startup
{
    public static class DataAccessSetup
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(provider =>
            {
                var config = provider.GetRequiredService<DatabaseSettings>();
                return new MongoClient(config.ConnectionString);
            });
            services.AddScoped<IMongoDatabase>(provider =>
            {
                var config = provider.GetRequiredService<DatabaseSettings>();
                var client = provider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(config.DatabaseName);
            });
            
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ILaLigaTableService, LaLigaTableService>();
        }
    }
}