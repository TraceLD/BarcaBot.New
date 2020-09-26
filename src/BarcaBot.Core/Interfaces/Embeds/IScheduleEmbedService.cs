using System.Collections.Generic;
using BarcaBot.Core.Models;
using Discord;

namespace BarcaBot.Core.Interfaces.Embeds
{
    public interface IScheduleEmbedService
    {
        EmbedBuilder CreateScheduleEmbed(IList<ScheduledMatch> scheduledMatches);
    }
}