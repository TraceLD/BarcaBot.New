using System.Collections.Generic;

namespace BarcaBot.Core.Models.FootballData.Scorers
{
    public class ScorersResponse
    {
        public int Count { get; set; }
        public Filters Filters { get; set; }
        public Competition Competition { get; set; }
        public Season Season { get; set; }
        public List<Scorer> Scorers { get; set; }
    }
}