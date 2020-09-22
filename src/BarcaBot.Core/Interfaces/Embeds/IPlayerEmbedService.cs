using BarcaBot.Core.Models.Player;
using Discord;

namespace BarcaBot.Core.Interfaces.Embeds
{
    public interface IPlayerEmbedService
    {
        Embed CreatePlayerEmbed(Player player);
    }
}