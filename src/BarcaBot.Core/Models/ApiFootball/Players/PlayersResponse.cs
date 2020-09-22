using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BarcaBot.Core.Models.ApiFootball.Players
{
    public class PlayersResponse
    {
        [JsonPropertyName("api")]
        public ApiValueDto Value { get; set; }
    }
    
    public class ApiValueDto
    {
        public int Results { get; set; }
        
        public IEnumerable<Player> Players { get; set; }
    }
}