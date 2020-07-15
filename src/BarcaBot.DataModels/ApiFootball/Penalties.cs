using System.Text.Json.Serialization;

namespace BarcaBot.DataModels.ApiFootball
{
    public class Penalties
    {
        public int Won { get; set; }
        
        [JsonPropertyName("commited")]
        public int Committed { get; set; }
        
        public int Success { get; set; }
        
        public int Missed { get; set; }
        
        public int Saved { get; set; }
    }
}