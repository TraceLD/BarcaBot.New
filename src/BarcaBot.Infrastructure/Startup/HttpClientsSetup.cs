using BarcaBot.Core.Interfaces.Http;
using BarcaBot.Infrastructure.Services.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BarcaBot.Infrastructure.Startup
{
    public static class HttpClientsSetup
    {
        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<IApiFootballService, ApiFootballService>();
            services.AddHttpClient<IFootballDataService, FootballDataService>();
        }
    }
}