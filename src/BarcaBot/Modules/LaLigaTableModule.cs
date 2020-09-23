using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Embeds;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Modules
{
    public class LaLigaTableModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<LaLigaTableModule> _logger;
        private readonly ILaLigaTableService _tableService;
        private readonly ILaLigaTableEmbedService _embedService;

        public LaLigaTableModule(ILogger<LaLigaTableModule> logger, ILaLigaTableService tableService, ILaLigaTableEmbedService embedService)
        {
            _logger = logger;
            _tableService = tableService;
            _embedService = embedService;
        }

        [Priority(-1)]
        [Command("table")]
        public async Task Table()
        {
            const string s = ":x: Incorrect arguments.\nUsage:\n" +
                             "`table top`\n" +
                             "`table bottom`";

            await Context.Channel.SendMessageAsync(s);
        }

        [Priority(1)]
        [Command("table top", RunMode = RunMode.Async)]
        public async Task TableTop()
        {
            var table = await _tableService.GetTop5();
            var embed = _embedService.CreateTableEmbed(table);
            embed.WithTitle("Current LaLiga Top 5");
            
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Priority(1)]
        [Command("table bottom", RunMode = RunMode.Async)]
        public async Task TableBottom()
        {
            var table = await _tableService.GetBottom5();
            var embed = _embedService.CreateTableEmbed(table);
            embed.WithTitle("Current LaLiga Bottom 5");
            
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}