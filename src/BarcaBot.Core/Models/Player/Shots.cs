using System.Text.Json.Serialization;

namespace BarcaBot.Core.Models.Player
{
    public class Shots
    {
        public int Total { get; set; }
        
        [JsonPropertyName("on")]
        public int OnTarget { get; set; }
    }
}