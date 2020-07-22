using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BarcaBot.Infrastructure.Extensions;
using BarcaBot.Infrastructure.Services;
using BarcaBot.Infrastructure.Services.Http;
using Discord.Commands;

namespace BarcaBot.Modules
{
    public class PlayerModule : ModuleBase<SocketCommandContext>
    {
        private readonly ApiFootballService _api;

        public PlayerModule(ApiFootballService api)
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