using BarcaBot.Infrastructure.HostedServices;
using BarcaBot.Infrastructure.HostedServices.Updaters;
using Microsoft.Extensions.DependencyInjection;

namespace BarcaBot.Infrastructure.Startup
{
    public static class UpdatersSetup
    {
        public static void AddAutoUpdaters(this IServiceCollection services)
        {
            services.AddHostedService<PlayersHostedService>();
            services.AddHostedService<LaLigaTableHostedService>();
            services.AddHostedService<LaLigaScorersHostedService>();
            services.AddHostedService<ScheduledMatchesHostedService>();
        }
    }
}