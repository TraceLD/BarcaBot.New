using System.Collections.Generic;

namespace BarcaBot.Core.Models.FootballData.Matches
{
    public class Filters
    {
        public string Permission { get; set; }
        public List<string> Status { get; set; }
        public int Limit { get; set; }
    }
}