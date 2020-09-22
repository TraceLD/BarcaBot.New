using System.Collections.Generic;
using BarcaBot.Core.Models.Table;

namespace BarcaBot.Core.Models.Dto.FootballData
{
    public class StandingDto
    {
        public string Stage { get; set; }
        public string Type { get; set; }
        public IEnumerable<TableDto> Table { get; set; }
    }
}