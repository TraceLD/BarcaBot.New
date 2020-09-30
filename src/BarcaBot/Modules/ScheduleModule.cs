using System.Linq;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Embeds;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Modules
{
    public class ScheduleModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<ScheduleModule> _logger;
        private readonly IScheduledMatchService _matchService;
        private readonly IScheduleEmbedService _embedService;
        private readonly IBasicEmbedsService _basicEmbedsService;

        public ScheduleModule(ILogger<ScheduleModule> logger, IScheduledMatchService matchService, IScheduleEmbedService embedService, IBasicEmbedsService basicEmbedsService)
        {
            _logger = logger;
            _matchService = matchService;
            _embedService = embedService;
            _basicEmbedsService = basicEmbedsService;
        }

        [Command("schedule", RunMode = RunMode.Async)]
        public async Task Schedule()
        {
            var schedule = await _matchService.GetAsync();

            if (!schedule.Any())
            {
                var errorEmbed = _basicEmbedsService.CreateErrorEmbed("No upcoming matches found.");
                await Context.Channel.SendMessageAsync("", false, errorEmbed.Build());
                return;
            }

            var embed = _embedService.CreateScheduleEmbed(schedule);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}