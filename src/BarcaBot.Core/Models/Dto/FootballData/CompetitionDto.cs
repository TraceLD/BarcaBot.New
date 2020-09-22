using System;
using System.Text.Json.Serialization;

namespace BarcaBot.Core.Models.Dto.FootballData
{
    public class CompetitionDto
    {
        public int Id { get; set; }
        
        public AreaDto Area { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public string Plan { get; set; }
        
        [JsonPropertyName("lastUpdated")]
        public DateTime UpdatedAt { get; set; }
    }
}