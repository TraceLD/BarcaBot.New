using System.Collections.Generic;
using Discord;

namespace BarcaBot.Core.Interfaces.Embeds
{
    public interface IBasicEmbedsService
    {
        EmbedBuilder CreateHelpEmbed(Dictionary<string, Dictionary<string, string>> commands);
        EmbedBuilder CreateErrorEmbed(string errorMsg);
        EmbedBuilder CreateUsageEmbed(params string[] usageExamples);
    }
}