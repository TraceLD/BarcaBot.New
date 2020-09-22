using System.Threading.Tasks;
using BarcaBot.Core.Models.Dto.FootballData;

namespace BarcaBot.Core.Interfaces.Http
{
    public interface IFootballDataService
    {
        Task<StandingsResponseDto> GetLaLigaStandings();
    }
}