using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BarcaBot.Core.Models
{
    public class Scorer
    {
        [BsonId]
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public string Name { get; set; }
        public string Nationality { get; set; }
        public Team Team { get; set; }
        public int Goals { get; set; } 
    }
}