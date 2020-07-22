using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Infrastructure.Extensions;
using Discord.Commands;

namespace BarcaBot.Modules
{
    public class PlayerModule : ModuleBase<SocketCommandContext>
    {
        private readonly IApiFootballService _api;

        public PlayerModule(IApiFootballService api)
        {
            _api = api;
        }
        
        [Command("player", RunMode = RunMode.Async)]
        public async Task Player()
        {
            var players = (await _api.GetPlayersAsync()).AsPlayers();
            
            var res = JsonSerializer.Serialize(players);
            
            await System.IO.File.WriteAllTextAsync(@"C:\Users\Lukasz\Documents\Git_Projects\BarcaBot.New\src\BarcaBot\result.json", res);
            
            await Context.Channel.SendMessageAsync("success");
        }
    }
}