using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Embeds;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Modules
{
    public class LaLigaScorersModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<LaLigaScorersModule> _logger;
        private readonly ILaLigaScorersService _scorersService;
        private readonly ILaLigaScorersEmbedService _embedService;

        public LaLigaScorersModule(ILogger<LaLigaScorersModule> logger, ILaLigaScorersService scorersService, ILaLigaScorersEmbedService embedService)
        {
            _logger = logger;
            _scorersService = scorersService;
            _embedService = embedService;
        }

        [Command("scorers", RunMode = RunMode.Async)]
        public async Task Scorers()
        {
            var scorers = await _scorersService.GetAsync();
            var embed = _embedService.CreateScorersEmbed(scorers);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}