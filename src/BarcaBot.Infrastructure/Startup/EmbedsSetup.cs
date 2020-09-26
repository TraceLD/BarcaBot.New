using BarcaBot.Core.Interfaces.Embeds;
using BarcaBot.Infrastructure.Services.Embeds;
using Microsoft.Extensions.DependencyInjection;

namespace BarcaBot.Infrastructure.Startup
{
    public static class EmbedsSetup
    {
        public static void AddEmbedServices(this IServiceCollection services)
        {
            services.AddSingleton<IPlayerEmbedService, PlayerEmbedService>();
            services.AddSingleton<ILaLigaTableEmbedService, LaLigaTableEmbedService>();
            services.AddSingleton<ILaLigaScorersEmbedService, LaLigaScorersEmbedService>();
            services.AddSingleton<IScheduleEmbedService, ScheduleEmbedService>();
        }
    }
}