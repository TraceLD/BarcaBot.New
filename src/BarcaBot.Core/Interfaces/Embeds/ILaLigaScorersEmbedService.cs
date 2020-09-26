using System.Collections.Generic;
using BarcaBot.Core.Models;
using Discord;

namespace BarcaBot.Core.Interfaces.Embeds
{
    public interface ILaLigaScorersEmbedService
    {
        EmbedBuilder CreateScorersEmbed(IList<Scorer> scorers);
    }
}