using System.Text.Json.Serialization;

namespace BarcaBot.DataModels.ApiFootball
{
    public class Games
    {
        [JsonPropertyName("appearences")]
        public int Appearances { get; set; }
        
        [JsonPropertyName("minutes_played")]
        public int MinutesPlayed { get; set; }
        
        [JsonPropertyName("lineups")]
        public int StartingEleven { get; set; }
    }
}