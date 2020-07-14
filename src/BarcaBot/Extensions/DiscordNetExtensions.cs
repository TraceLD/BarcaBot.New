using Discord;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Extensions
{
    public static class DiscordNetExtensions
    {
        /// <summary>
        /// Converts Discord.LogSeverity to Microsoft.Extensions.Logging.LogLevel
        /// </summary>
        /// <param name="severity">LogSeverity to convert</param>
        /// <returns>Converted LogLevel</returns>
        public static LogLevel ToLogLevel(this LogSeverity severity)
        {
            return severity switch
            {
                LogSeverity.Critical => LogLevel.Critical,
                LogSeverity.Error => LogLevel.Error,
                LogSeverity.Warning => LogLevel.Warning,
                LogSeverity.Info => LogLevel.Information,
                LogSeverity.Verbose => LogLevel.Debug,
                LogSeverity.Debug => LogLevel.Debug,
                _ => LogLevel.Debug
            };
        }
    }
}