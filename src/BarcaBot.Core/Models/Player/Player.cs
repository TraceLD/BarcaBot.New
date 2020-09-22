using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BarcaBot.Core.Models.Player
{
    public class Player
    {
        [BsonId]
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; } 
        
        public string Name { get; set; }
        public int? Number { get; set; }
        public Position Position { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        
        public PlayerStatistics Statistics { get; set; }
    }
}