using System.Collections.Generic;
using System.Text;
using BarcaBot.Core.Interfaces.Embeds;
using BarcaBot.Core.Models.Settings;
using Discord;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Infrastructure.Services.Embeds
{
    public class BasicEmbedsService : IBasicEmbedsService
    {
        private readonly ILogger<BasicEmbedsService> _logger;
        private readonly DiscordSettings _discordSettings;

        public BasicEmbedsService(ILogger<BasicEmbedsService> logger, DiscordSettings discordSettings)
        {
            _logger = logger;
            _discordSettings = discordSettings;
        }

        public EmbedBuilder CreateHelpEmbed(Dictionary<string, Dictionary<string, string>> commands)
        {
            var prefix = _discordSettings.Prefix;
            var builder = new EmbedBuilder()
                .WithTitle(":information_source: Help")
                .WithColor(Color.Blue)
                .WithFooter("BarcaBot by Trace (@Trace#8994 on Discord, TraceLd on GitHub)");

            foreach (var (cmdCategory, commandsForThatCategory) in commands)
            {
                var commandsString = new StringBuilder();
                
                foreach (var (commandName, commandDescription) in commandsForThatCategory)
                {
                    commandsString.Append($"{prefix}{commandName}\n{commandDescription}\n\n");
                }

                builder.AddField(cmdCategory, commandsString);
            }

            return builder;
        }
    }
}