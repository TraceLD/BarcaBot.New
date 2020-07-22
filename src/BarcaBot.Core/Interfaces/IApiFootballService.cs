using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Models.Dto.ApiFootball;

namespace BarcaBot.Core.Interfaces
{
    public interface IApiFootballService
    {
        Task<IEnumerable<PlayerDto>> GetPlayersAsync();
    }
}