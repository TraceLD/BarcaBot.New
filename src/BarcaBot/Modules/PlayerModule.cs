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
        private readonly IPlayerEmbedService _playerEmbedService;

        public PlayerModule(ILogger<PlayerModule> logger, IPlayerService playerService, ICountryEmojiService emojiService, IPlayerEmbedService playerEmbedService)
        {
            _logger = logger;
            _playerService = playerService;
            _playerEmbedService = playerEmbedService;
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
            
            var embed = _playerEmbedService.CreatePlayerEmbed(player);
            
            await Context.Channel.SendMessageAsync("", false, embed);
        }
    }
}