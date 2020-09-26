using System.Threading.Tasks;
using BarcaBot.Core.Models.FootballData.Matches;
using BarcaBot.Core.Models.FootballData.Scorers;
using BarcaBot.Core.Models.FootballData.Table;

namespace BarcaBot.Core.Interfaces.Http
{
    public interface IFootballDataService
    {
        Task<StandingsResponse> GetLaLigaStandings();
        Task<MatchesResponse> GetScheduledMatches();
        Task<ScorersResponse> GetLaLigaScorers();
    }
}