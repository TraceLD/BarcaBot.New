using System;
using System.Text.Json.Serialization;
using BarcaBot.Core.Json;
using BarcaBot.Core.Models.Player;

namespace BarcaBot.Core.Models.Dto.ApiFootball
{
    public class PlayerDto
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }
        
        [JsonPropertyName("player_name")]
        public string PlayerName { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int? Number { get; set; }
        
        public Position Position { get; set; }
        
        public int Age { get; set; }
        
        [JsonPropertyName("birth_date")]
        public DateTime BirthDate { get; set; }
        
        [JsonPropertyName("birth_place")]
        public string BirthPlace { get; set; }
        
        [JsonPropertyName("birth_country")]
        public string BirthCountry { get; set; }
        
        public string Nationality { get; set; }
        
        public string Height { get; set; }
        
        public string Weight { get; set; }
        
        public bool? Injured { get; set; }
        
        [JsonConverter(typeof(StringDoubleConverter))]
        public double? Rating { get; set; }
        
        [JsonPropertyName("team_id")]
        public int TeamId { get; set; }
        
        [JsonPropertyName("team_name")]
        public string TeamName { get; set; }
        
        public string League { get; set; }
        
        public string Season { get; set; }
        
        public int Captain { get; set; }
        
        public Shots Shots { get; set; }
        
        public Goals Goals { get; set; }
        
        public Passes Passes { get; set; }
        
        public Tackles Tackles { get; set; }
        
        public Duels Duels { get; set; }
        
        public Dribbles Dribbles { get; set; }
        
        public Fouls Fouls { get; set; }
        
        public Cards Cards { get; set; }
        
        [JsonPropertyName("penalty")]
        public Penalties Penalties { get; set; }
        
        public Games Games { get; set; }
        
        public Substitutes Substitutes { get; set; }
    }
}