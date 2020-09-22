using System.Collections.Generic;

namespace BarcaBot.Core.Models.Dto.FootballData
{
    public class StandingsResponseDto
    {
        public object Filters { get; set; }
        public CompetitionDto Competition { get; set; }
        public SeasonDto Season { get; set; }
        public IEnumerable<StandingDto> Standings { get; set; }
    }
}