using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Models.ApiFootball.Players;

namespace BarcaBot.Core.Interfaces.Http
{
    public interface IApiFootballService
    {
        Task<IEnumerable<Player>> GetPlayerDtosAsync();
    }
}