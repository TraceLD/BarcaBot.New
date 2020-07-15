using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Discord.Commands;

using BarcaBot.Services.Http;

namespace BarcaBot.Commands
{
    public class PlayerCommand : ModuleBase<SocketCommandContext>
    {
        private readonly ApiFootballService _api;

        public PlayerCommand(ApiFootballService api)
        {
            _api = api;
        }
        
        [Command("player", RunMode = RunMode.Async)]
        public async Task Player()
        {
            var players = await _api.GetPlayers();

            await Context.Channel.SendMessageAsync(players.First().PlayerName);
        }
    }
}