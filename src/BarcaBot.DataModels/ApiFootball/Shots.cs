using System.Text.Json.Serialization;

namespace BarcaBot.DataModels.ApiFootball
{
    public class Shots
    {
        public int Total { get; set; }
        
        [JsonPropertyName("on")]
        public int OnTarget { get; set; }
    }
}