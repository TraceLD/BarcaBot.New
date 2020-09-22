using System.Collections.Generic;

namespace BarcaBot.Core.Models.FootballData.Matches
{
    public class MatchesResponse
    {
        public int Count { get; set; }
        public Filters Filters { get; set; }
        public List<Match> Matches { get; set; }
    }
}