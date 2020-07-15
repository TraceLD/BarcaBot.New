using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BarcaBot.DataModels.ApiFootball
{
    public class PlayersResponse
    {
        [JsonPropertyName("api")]
        public Value Value { get; set; }
    }
    
    public class Value
    {
        public int Results { get; set; }
        
        public IEnumerable<Player> Players { get; set; }
    }
}