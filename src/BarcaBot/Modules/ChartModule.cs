using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Player;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Modules
{
    public class ChartModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<ChartModule> _logger;
        private readonly IChartService _chartService;
        private readonly IPlayerService _playerService;

        public ChartModule(ILogger<ChartModule> logger, IChartService chartService, IPlayerService playerService)
        {
            _logger = logger;
            _chartService = chartService;
            _playerService = playerService;
        }

        [Priority(1)]
        [Command("stats")]
        public async Task Stats()
        {
            const string s = ":x: Incorrect arguments.\nUsage:\n" +
                             "`stats <player_name>`\n" +
                             "`stats <player_name> <player_name2>`" +
                             "`stats <player_name> <player_name2> <player_name3> etc.`";
            
            await Context.Channel.SendMessageAsync(s);
        }

        [Priority(-1)]
        [Command("stats", RunMode = RunMode.Async)]
        public async Task Stats(params string[] names)
        {
            var players = new List<Player>();

            foreach (var name in names)
            {
                var player = await _playerService.GetAsync(name);
                
                if (player is null)
                {
                    await Context.Channel.SendMessageAsync(
                        $":x: Could not find player `{name}`." +
                        " Are you sure they exist and are a Barca player?\n" +
                        "If you think there is a player missing from the database please report it to the creator of BarcaBot: `Trace#8994`.");
                    return;
                }
                
                players.Add(player);
            }

            var chart = await _chartService.GetStatsBarChart(players);
            var chartStream = new MemoryStream(chart);
            await Context.Channel.SendFileAsync(chartStream, "chart.png");
        }
    }
}