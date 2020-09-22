namespace BarcaBot.Core.Models.Player
{
    public class PlayerStatistics
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