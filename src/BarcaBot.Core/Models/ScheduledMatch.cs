using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BarcaBot.Core.Models
{
    public class ScheduledMatch
    {
        [BsonId]
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Competition Competition { get; set; }
        public Season Season { get; set; }
        public DateTime UtcDate { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }
}