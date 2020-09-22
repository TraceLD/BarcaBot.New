using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Models.Dto.FootballData;
using BarcaBot.Core.Models.Table;

namespace BarcaBot.Core.Interfaces
{
    public interface IFootballDataService
    {
        Task<StandingsResponseDto> GetLaLigaStandings();
    }
}