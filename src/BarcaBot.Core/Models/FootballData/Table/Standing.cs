using System.Collections.Generic;

namespace BarcaBot.Core.Models.FootballData.Table
{
    public class Standing
    {
        public string Stage { get; set; }
        public string Type { get; set; }
        public IEnumerable<Table> Table { get; set; }
    }
}