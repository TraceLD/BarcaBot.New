using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Embeds;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Modules
{
    public class PlayerModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<PlayerModule> _logger;
        private readonly IPlayerService _playerService;
        private readonly IPlayerEmbedService _embedService;
        private readonly IBasicEmbedsService _basicEmbedsService;

        public PlayerModule(ILogger<PlayerModule> logger, IPlayerService playerService, ICountryEmojiService emojiService, IPlayerEmbedService embedService, IBasicEmbedsService basicEmbedsService)
        {
            _logger = logger;
            _playerService = playerService;
            _embedService = embedService;
            _basicEmbedsService = basicEmbedsService;
        }

        [Priority(-1)]
        [Command("player")]
        public async Task Player()
        {
            var embed = _basicEmbedsService.CreateUsageEmbed("player <name>");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        
        [Priority(1)]
        [Command("player", RunMode = RunMode.Async)]
        public async Task Player([Remainder]string name)
        {
            var player = await _playerService.GetAsync(name);
            
            if (player is null)
            {
                var errorEmbed = _basicEmbedsService.CreateErrorEmbed($"Could not find player `{name}`." +
                                                                      " Are you sure they exist and are a Barca player?\n\n" +
                                                                      "If you think there is a player missing from the database please report it to the creator of BarcaBot: `Trace#8994` or open a GitHub issue.");
                await Context.Channel.SendMessageAsync($"", false, errorEmbed.Build());
                return;
            }
            
            var embed = _embedService.CreatePlayerEmbed(player);
            
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}