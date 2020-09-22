#nullable enable
namespace BarcaBot.Core.Models.FootballData.Matches
{
    public class Score
    {
        public string? Winner { get; set; }
        public string Duration { get; set; } = null!;
        public string Result { get; set; } = null!;
    }
}