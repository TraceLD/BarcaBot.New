namespace BarcaBot.Core.Models.FootballData.Scorers
{
    public class Scorer
    {
        public Player Player { get; set; }
        public Team Team { get; set; }
        public int NumberOfGoals { get; set; }
    }
}