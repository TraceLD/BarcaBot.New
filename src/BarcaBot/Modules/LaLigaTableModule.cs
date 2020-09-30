using System.Linq;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Embeds;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Modules
{
    public class LaLigaTableModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<LaLigaTableModule> _logger;
        private readonly ILaLigaTableService _tableService;
        private readonly ILaLigaTableEmbedService _embedService;
        private readonly IBasicEmbedsService _basicEmbedsService;

        private readonly Embed _errorEmbed; 

        public LaLigaTableModule(ILogger<LaLigaTableModule> logger, ILaLigaTableService tableService, ILaLigaTableEmbedService embedService, IBasicEmbedsService basicEmbedsService)
        {
            _logger = logger;
            _tableService = tableService;
            _embedService = embedService;
            _basicEmbedsService = basicEmbedsService;

            _errorEmbed = _basicEmbedsService.CreateErrorEmbed("Error while obtaining the table.").Build();
        }

        [Priority(-1)]
        [Command("table")]
        public async Task Table()
        {
            var embed = _basicEmbedsService.CreateUsageEmbed("table top", "table bottom");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Priority(1)]
        [Command("table top", RunMode = RunMode.Async)]
        public async Task TableTop()
        {
            var table = await _tableService.GetTop5();
            
            if (!table.Any())
            {
                await Context.Channel.SendMessageAsync("", false, _errorEmbed);
                return;
            }

            var embed = _embedService.CreateTableEmbed(table)
                .WithTitle("Current LaLiga Top 5");
            
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Priority(1)]
        [Command("table bottom", RunMode = RunMode.Async)]
        public async Task TableBottom()
        {
            var table = await _tableService.GetBottom5();
            
            if (!table.Any())
            {
                await Context.Channel.SendMessageAsync("", false, _errorEmbed);
                return;
            }

            var embed = _embedService.CreateTableEmbed(table)
                .WithTitle("Current LaLiga Bottom 5");
            
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}