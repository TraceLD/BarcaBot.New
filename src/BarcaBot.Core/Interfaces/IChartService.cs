using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Models.Player;

namespace BarcaBot.Core.Interfaces
{
    public interface IChartService
    {
        Task<byte[]> GetStatsBarChart(IList<Player> players);
    }
}