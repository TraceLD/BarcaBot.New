using System.Collections.Generic;
using BarcaBot.Core.Models.Table;
using Discord;

namespace BarcaBot.Core.Interfaces.Embeds
{
    public interface ILaLigaTableEmbedService
    {
        EmbedBuilder CreateTableEmbed(IEnumerable<TablePosition> leagueTable);
    }
}