using System;
using System.Threading.Tasks;

using Discord.Commands;

namespace BarcaBot.Commands
{
    public class HelpCommand  : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Help()
        {
            throw new NotImplementedException();
        }
    }
}