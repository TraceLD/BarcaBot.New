using System.Reflection;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Embeds;
using BarcaBot.Infrastructure.HostedServices;
using BarcaBot.Infrastructure.Services.Embeds;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BarcaBot.Infrastructure.Startup
{
    public static class BotSetup
    {
        public static void AddDiscordBot(this IServiceCollection services)
        {
            services.AddSingleton<IBotService, BotHostedService>();
            var commandService = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Sync,
                LogLevel = LogSeverity.Verbose
            });
            services.AddSingleton(provider =>
            {
                commandService.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
                return commandService;
            });
            
            services.AddHostedService(provider => provider.GetRequiredService<IBotService>());
            services.AddHostedService<CommandHostedService>();
            
            // add embeds;
            services.AddSingleton<IPlayerEmbedService, PlayerEmbedService>();
            services.AddSingleton<ILaLigaTableEmbedService, LaLigaTableEmbedService>();
            services.AddSingleton<ILaLigaScorersEmbedService, LaLigaScorersEmbedService>();
            services.AddSingleton<IScheduleEmbedService, ScheduleEmbedService>();
            services.AddSingleton<IBasicEmbedsService, BasicEmbedsService>();
            
            // add cmds descriptions;
            services.AddSingleton<CommandsDescriptions>();
        }
    }
}