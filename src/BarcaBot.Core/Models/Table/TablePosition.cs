using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BarcaBot.Core.Models.Table
{
    public class TablePosition
    {
        [BsonId]
        public int Id { get; set; }
        public int CurrentMatchday { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Team Team { get; set; }
        public TeamStatistics TeamStatistics { get; set; }
    }
}