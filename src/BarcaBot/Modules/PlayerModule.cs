using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Player;
using BarcaBot.Infrastructure;
using BarcaBot.Infrastructure.Extensions;
using BarcaBot.Infrastructure.Services;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Modules
{
    public class PlayerModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<PlayerModule> _logger;
        private readonly IPlayerService _playerService;
        private readonly ICountryEmojiService _emojiService;

        public PlayerModule(ILogger<PlayerModule> logger, IPlayerService playerService, ICountryEmojiService emojiService)
        {
            _logger = logger;
            _playerService = playerService;
            _emojiService = emojiService;
        }

        [Priority(-1)]
        [Command("player")]
        public async Task Player()
        {
            const string s = ":x: Incorrect arguments.\nUsage: `player <name>`";
            
            await Context.Channel.SendMessageAsync(s);
        }
        
        [Priority(1)]
        [Command("player", RunMode = RunMode.Async)]
        public async Task Player([Remainder]string name)
        {
            var player = await _playerService.GetAsync(name);
            if (player is null)
            {
                await Context.Channel.SendMessageAsync($":x: Cannot find a player with name {name}");
                return;
            }
            var embed = CreateEmbedBuilder(player).Build();
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        private EmbedBuilder CreateEmbedBuilder(Player player)
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
                .AddField("Rating", $"{stats.Rating:0.##}");
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

            return builder;

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