using System.Threading.Tasks;
using BarcaBot.Core.Interfaces.Embeds;
using BarcaBot.Infrastructure;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Modules
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<HelpModule> _logger;
        private readonly IBasicEmbedsService _embedsService;
        private readonly CommandsDescriptions _descriptions;

        public HelpModule(ILogger<HelpModule> logger, IBasicEmbedsService embedsService, CommandsDescriptions descriptions)
        {
            _logger = logger;
            _embedsService = embedsService;
            _descriptions = descriptions;
        }

        [Command("help", RunMode = RunMode.Async)]
        public async Task Help()
        {
            var embed = _embedsService.CreateHelpEmbed(_descriptions.CommandCategories).Build();
            await Context.Channel.SendMessageAsync("", false, embed);
        }
    }
}