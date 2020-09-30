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
        private readonly char _prefix;

        public BasicEmbedsService(ILogger<BasicEmbedsService> logger, DiscordSettings discordSettings)
        {
            _logger = logger;
            _prefix = discordSettings.Prefix;
        }

        public EmbedBuilder CreateHelpEmbed(Dictionary<string, Dictionary<string, string>> commands)
        {
            var builder = new EmbedBuilder()
                .WithTitle(":information_source: Help")
                .WithColor(Color.Blue)
                .WithFooter("BarcaBot by Trace (@Trace#8994 on Discord, TraceLd on GitHub)");

            foreach (var (cmdCategory, commandsForThatCategory) in commands)
            {
                var commandsString = new StringBuilder();
                
                foreach (var (commandName, commandDescription) in commandsForThatCategory)
                {
                    commandsString.Append($"{_prefix}{commandName}\n{commandDescription}\n\n");
                }

                builder.AddField(cmdCategory, commandsString);
            }

            return builder;
        }

        public EmbedBuilder CreateErrorEmbed(string errorMsg)
        {
            var builder = new EmbedBuilder()
                .WithTitle(":warning: Error")
                .WithColor(Color.Red)
                .WithDescription(errorMsg);

            return builder;
        }

        public EmbedBuilder CreateUsageEmbed(params string[] usageExamples)
        {
            
            var builder = new EmbedBuilder()
                .WithTitle(":information_source: Usage")
                .WithColor(Color.DarkBlue)
                .WithDescription(
                    "You've attempted to use this command incorrectly. Below are correct usage examples for this command");

            var examplesString = new StringBuilder();
            
            foreach (var usageExample in usageExamples)
            {
                examplesString.Append($"`{_prefix}{usageExample}`\n");
            }

            builder.AddField("Usage examples", examplesString);

            return builder;
        }
    }
}