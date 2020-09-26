using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Player;
using Microsoft.Extensions.Logging;
using TraceLd.PlotlySharp;
using TraceLd.PlotlySharp.Api;
using TraceLd.PlotlySharp.Api.Traces;

namespace BarcaBot.Infrastructure.Services
{
    public class ChartService : IChartService
    {
        private readonly ILogger<ChartService> _logger;
        private readonly PlotlyClient _plotlyClient;

        public ChartService(ILogger<ChartService> logger, PlotlyClient plotlyClient)
        {
            _logger = logger;
            _plotlyClient = plotlyClient;
        }

        public async Task<byte[]> GetStatsBarChart(IList<Player> players)
        {
            var chart = new PlotlyChart
            {
                Figure = new Figure
                {
                    Data = new ArrayList()
                },
                Height = 500,
                Width = 1000
            };
            var chartLayout = GetDefaultLayout();
            var x = new ArrayList
            {
                "Shots", "Shots on Target", "Key Passes", "Tackles", "Blocks", "Interceptions", "Duels Won",
                "Dribbles Attempted", "Dribbles Won", "Fouls Drawn", "Fouls Committed"
            };

            foreach (var player in players)
            {
                var stats = player.Statistics;
                var converter = new ToPer90Converter(stats.Games.MinutesPlayed);
                var y = new ArrayList
                {
                    converter.ToPer90(stats.Shots.Total),
                    converter.ToPer90(stats.Shots.OnTarget),
                    converter.ToPer90(stats.Passes.Key),
                    converter.ToPer90(stats.Tackles.Total),
                    converter.ToPer90(stats.Tackles.Blocks),
                    converter.ToPer90(stats.Tackles.Interceptions),
                    converter.ToPer90(stats.Duels.Won),
                    converter.ToPer90(stats.Dribbles.Attempts),
                    converter.ToPer90(stats.Dribbles.Success),
                    converter.ToPer90(stats.Fouls.Drawn),
                    converter.ToPer90(stats.Fouls.Committed)
                };
                var trace = new BarTrace
                {
                    X = x,
                    Y = y,
                    Name = player.Name
                };

                chart.Figure.Data.Add(trace);
            }

            if (players.Count == 1)
            {
                chartLayout.Title.Text = $"{players[0].Name} per 90 statistics";
                chartLayout.ShowLegend = false;
            }
            else
            {
                chartLayout.Title.Text = "Per 90 statistics comparison";
                chartLayout.ShowLegend = true;
            }

            chart.Figure.Layout = chartLayout;

            return await _plotlyClient.GetChartAsByteArray(chart);
        }

        private Layout GetDefaultLayout()
        {
            return new Layout
            {
                Colorway = new List<string>
                {
                    "#636efa",
                    "#EF553B",
                    "#00cc96",
                    "#ab63fa",
                    "#19d3f3",
                    "#e763fa",
                    "#fecb52",
                    "#ffa15a",
                    "#ff6692",
                    "#b6e880"
                },
                Title = new Title(),
                PaperBgColor = "#1a1a23",
                PlotBgColor = "#1a1a23",
                Font = new Font
                {
                    Color = "#ebebeb"
                },
            };
        }
    }
}