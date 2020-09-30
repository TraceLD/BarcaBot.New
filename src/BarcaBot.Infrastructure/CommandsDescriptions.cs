using System.Collections.Generic;

namespace BarcaBot.Infrastructure
{
    public class CommandsDescriptions
    {
        public Dictionary<string, Dictionary<string, string>> CommandCategories => new Dictionary<string, Dictionary<string, string>>
        {
            {"Player info", PlayerInfoCmds},
            {"FC Barcelona matches", FcbMatchesCmds},
            {"LaLiga table", LaLigaTableCmds},
            {"LaLiga top scorers", LaLigaScorersCmds},
            {"Statistics", StatisticsCmds},
            {"Other", OtherCmds}
        };
        
        private Dictionary<string, string> PlayerInfoCmds { get; } = new Dictionary<string, string>
        {
            {"player *<player_name>*", "Shows information (including statistics) about a Barca player."}
        };
        private Dictionary<string, string> FcbMatchesCmds { get; } = new Dictionary<string, string>
        {
            {"schedule", "Shows upcoming Barca matches."}
        };
        private Dictionary<string, string> LaLigaTableCmds { get; } = new Dictionary<string, string>
        {
            {"table top", "Shows LaLiga's current Top 5."},
            {"table bottom", "Shows LaLiga's current Bottom 5."}
        };
        private Dictionary<string, string> LaLigaScorersCmds { get; } = new Dictionary<string, string>
        {
            {"scorers", "Shows LaLiga's current top scorers."}
        };
        private Dictionary<string, string> StatisticsCmds { get; } = new Dictionary<string, string>
        {
            {"stats *<player_name>*", "Shows statistics chart for a given player."},
            {"stats *<player_name>* *<player_name2>* *<player_name3>* etc.", "Shows statistics chart comparing given players."}
        };
        private Dictionary<string, string> OtherCmds { get; } = new Dictionary<string, string>
        {
            {"help", "Shows all available commands"}
        };
    }
}