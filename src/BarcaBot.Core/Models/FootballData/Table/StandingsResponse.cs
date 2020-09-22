using System.Collections.Generic;

namespace BarcaBot.Core.Models.FootballData.Table
{
    public class StandingsResponse
    {
        public object Filters { get; set; }
        public Competition Competition { get; set; }
        public Season Season { get; set; }
        public IEnumerable<Standing> Standings { get; set; }
    }
}