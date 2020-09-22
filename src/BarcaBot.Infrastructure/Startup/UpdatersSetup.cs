using BarcaBot.Infrastructure.HostedServices;
using Microsoft.Extensions.DependencyInjection;

namespace BarcaBot.Infrastructure.Startup
{
    public static class UpdatersSetup
    {
        public static void AddAutoUpdaters(this IServiceCollection services)
        {
            services.AddHostedService<PlayersHostedService>();
            services.AddHostedService<LaLigaTableHostedService>();
            services.AddHostedService<ScheduledMatchesHostedService>();
        }
    }
}