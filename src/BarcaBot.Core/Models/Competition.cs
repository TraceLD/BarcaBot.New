using System;
using System.Text.Json.Serialization;
using BarcaBot.Core.Models.FootballData;

namespace BarcaBot.Core.Models
{
    public class Competition
    {
        public int Id { get; set; }
        
        public Area Area { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public string Plan { get; set; }
        
        [JsonPropertyName("lastUpdated")]
        public DateTime UpdatedAt { get; set; }
    }
}