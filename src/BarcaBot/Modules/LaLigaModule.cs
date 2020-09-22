using System.Text.Json;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Modules
{
    public class LaLigaModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<PlayerModule> _logger;
        private readonly IFootballDataService _footballDataService;

        public LaLigaModule(ILogger<PlayerModule> logger, IFootballDataService footballDataService)
        {
            _logger = logger;
            _footballDataService = footballDataService;
        }

        [Command("laliga", RunMode = RunMode.Async)]
        public async Task LaLiga()
        {
            var response = await _footballDataService.GetLaLigaStandings();
            var o = JsonSerializer.Serialize(response);
            System.IO.File.WriteAllText("./out.json", o);
            await Context.Channel.SendMessageAsync("hello");
        }
    }
}