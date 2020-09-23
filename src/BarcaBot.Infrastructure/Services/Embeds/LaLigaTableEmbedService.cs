using System.Collections.Generic;
using System.Linq;
using BarcaBot.Core.Interfaces.Embeds;
using BarcaBot.Core.Models.Table;
using Discord;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Infrastructure.Services.Embeds
{
    public class LaLigaTableEmbedService : ILaLigaTableEmbedService
    {
        private readonly ILogger<LaLigaTableEmbedService> _logger;

        public LaLigaTableEmbedService(ILogger<LaLigaTableEmbedService> logger)
        {
            _logger = logger;
        }

        public EmbedBuilder CreateTableEmbed(IEnumerable<TablePosition> leagueTable)
        {
            var table = leagueTable.ToList();
            
            var builder = new EmbedBuilder()
                .WithColor(Color.Orange)
                .WithThumbnailUrl("https://assets.laliga.com/assets/logos/laliga-v-negativo/laliga-v-negativo-1200x1200.jpg")
                .WithFooter($"Stats updated at {table.First().UpdatedAt:dd/MM/yyyy HH:mm} UTC");
            
            foreach (var tablePosition in table)
            {
                builder.AddField($"{tablePosition.Id}. {tablePosition.Team.Name}",
                    $"*Pts:* {tablePosition.TeamStatistics.Points}; " +
                    $"*W:* {tablePosition.TeamStatistics.Won}; " +
                    $"*D:* {tablePosition.TeamStatistics.Draw}; " +
                    $"*L:* {tablePosition.TeamStatistics.Lost}; " +
                    $"*GD:* {tablePosition.TeamStatistics.GoalDifference}");
            }

            return builder;
        }
    }
}