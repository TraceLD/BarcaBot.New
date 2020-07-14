namespace BarcaBot.DataModels.Core
{
    public class Settings
    {
        public DiscordSettings Discord { get; set; }
        public DatabaseSettings Database { get; set; }
        public ApisSettings Apis { get; set; }
    }
}