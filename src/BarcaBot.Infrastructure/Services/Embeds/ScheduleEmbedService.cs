using System.Collections.Generic;
using System.Linq;
using BarcaBot.Core.Interfaces.Embeds;
using BarcaBot.Core.Models;
using Discord;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Infrastructure.Services.Embeds
{
    public class ScheduleEmbedService : IScheduleEmbedService
    {
        private readonly ILogger<ScheduleEmbedService> _logger;

        public ScheduleEmbedService(ILogger<ScheduleEmbedService> logger)
        {
            _logger = logger;
        }

        public EmbedBuilder CreateScheduleEmbed(IList<ScheduledMatch> scheduledMatches)
        {
            var builder = new EmbedBuilder()
                .WithTitle("Upcoming FC Barcelona matches")
                .WithColor(Color.Purple)
                .WithThumbnailUrl("https://upload.wikimedia.org/wikipedia/en/thumb/4/47/FC_Barcelona_%28crest%29.svg/1200px-FC_Barcelona_%28crest%29.svg.png")
                .WithFooter($"Stats updated at {scheduledMatches.First().UpdatedAt:dd/MM/yyyy HH:mm} UTC");

            foreach (var scheduledMatch in scheduledMatches)
            {
                builder.AddField($"{scheduledMatch.HomeTeam.Name} - {scheduledMatch.AwayTeam.Name}",
                    $"{scheduledMatch.Competition.Name}\n" +
                    $"{scheduledMatch.UtcDate:dd/MM/yyyy HH:mm} UTC");
            }

            return builder;
        }
    }
}