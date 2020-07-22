using MongoDB.Bson.Serialization.Attributes;

namespace BarcaBot.Core.Models.Player
{
    public class Player
    {
        [BsonId]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int? Number { get; set; }
        public Position Position { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        
        public Statistics Statistics { get; set; }
    }

    public class Statistics
    {
        public double? Rating { get; set; }
        public Shots Shots { get; set; }
        public Goals Goals { get; set; }
        public Passes Passes { get; set; }
        public Tackles Tackles { get; set; }
        public Duels Duels { get; set; }
        public Dribbles Dribbles { get; set; }
        public Fouls Fouls { get; set; }
        public Cards Cards { get; set; }
        public Penalties Penalties { get; set; }
        public Games Games { get; set; }
        public Substitutes Substitutes { get; set; }
    }
}