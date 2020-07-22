using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Models.Player;

namespace BarcaBot.Core.Interfaces
{
    public interface IPlayerService
    {
        Task<IList<Player>> GetAsync();
        Task<Player> GetAsync(int id);
        Task<Player> GetAsync(string name);
        Task UpsertAsync(Player playerIn);
    }
}