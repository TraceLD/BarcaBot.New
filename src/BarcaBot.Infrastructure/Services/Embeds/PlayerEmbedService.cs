using System;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Embeds;
using BarcaBot.Core.Models.Player;
using Discord;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Infrastructure.Services.Embeds
{
    public class PlayerEmbedService : IPlayerEmbedService
    {
        private readonly ILogger<PlayerEmbedService> _logger;
        private readonly ICountryEmojiService _emojiService;

        public PlayerEmbedService(ILogger<PlayerEmbedService> logger, ICountryEmojiService emojiService)
        {
            _logger = logger;
            _emojiService = emojiService;
        }
        
        public Embed CreatePlayerEmbed(Player player)
        {
            var stats = player.Statistics;
            var flag = _emojiService.GetEmojiString(player.Nationality);
            var builder = new EmbedBuilder()
                .WithTitle($"{player.Name} {flag}")
                .WithColor(Color.Purple)
                .WithDescription(
                    $"Position: {player.Position.ToString()}, Age: {player.Age}, Height: {player.Height}, Weight: {player.Weight}"
                )
                .WithFooter($"Stats last updated at {player.UpdatedAt:dd/MM/yyyy HH:mm} UTC")
                .AddField("Games played", stats.Games.Appearances)
                .AddField("Goals", stats.Goals.Total)
                .AddField("Assists", stats.Goals.Assists)
                .AddField("Rating", stats.Rating is null ? "0" : $"{stats.Rating:0.##}");
            
            var converter = new ToPer90Converter(stats.Games.MinutesPlayed);
            
            switch (player.Position)
            {
                case Position.Attacker:
                    AddShots();
                    AddPasses();
                    AddDribbles();
                    break;
                case Position.Midfielder:
                    AddPasses();
                    AddDribbles();
                    AddTackles();
                    break;
                case Position.Defender:
                    AddGoalsConceded();
                    AddTackles();
                    AddPasses();
                    break;
                case Position.Goalkeeper:
                    AddGoalsConceded();
                    AddPasses();
                    break;
                case Position.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return builder.Build();

            void AddShots()
            {
                var shotsTotal = converter.ToPer90(stats.Shots.Total);
                var shotsOnTarget = converter.ToPer90(stats.Shots.OnTarget);
                var percentageShotsOnTarget = shotsOnTarget / shotsTotal;
                builder.AddField("Shots per 90 minutes",
                    $"Total: {shotsTotal:0.##}, On Target: {shotsOnTarget:0.##} ({percentageShotsOnTarget:0.##%})");
            }

            void AddPasses()
            {
                builder.AddField("Passes per 90 minutes",
                    $"Total: {converter.ToPer90(stats.Passes.Total):0.##}, Key passes: {converter.ToPer90(stats.Passes.Key):0.##}, Accuracy: {stats.Passes.Accuracy:0.##}%"
                );
            }

            void AddDribbles()
            {
                var dribblesAttempted = converter.ToPer90(stats.Dribbles.Attempts);
                var dribblesSuccessful = converter.ToPer90(stats.Dribbles.Success);
                var percentageDribblesSuccessful = dribblesSuccessful / dribblesAttempted;
                builder.AddField("Dribbles per 90 minutes",
                    $"Attempted: {dribblesAttempted:0.##}, Successful: {converter.ToPer90(stats.Dribbles.Success):0.##} ({percentageDribblesSuccessful:0.##%})"
                );
            }

            void AddTackles()
            {
                builder.AddField("Tackles per 90 minutes",
                    $"Tackles: {converter.ToPer90(stats.Tackles.Total):0.##}, Interceptions: {converter.ToPer90(stats.Tackles.Interceptions):0.##}, Blocks: {converter.ToPer90(stats.Tackles.Blocks):0.##}"
                );
            }

            void AddGoalsConceded()
            {
                builder.AddField("Goals conceded:", stats.Goals.Conceded);
            }
        }
    }
}