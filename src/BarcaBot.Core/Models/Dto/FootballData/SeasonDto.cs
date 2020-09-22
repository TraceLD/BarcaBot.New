using System;

namespace BarcaBot.Core.Models.Dto.FootballData
{
    public class SeasonDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CurrentMatchday { get; set; }
    }
}