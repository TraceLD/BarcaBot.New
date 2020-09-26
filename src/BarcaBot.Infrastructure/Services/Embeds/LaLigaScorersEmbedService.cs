using System.Collections.Generic;
using System.Linq;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Embeds;
using BarcaBot.Core.Models;
using Discord;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Infrastructure.Services.Embeds
{
    public class LaLigaScorersEmbedService : ILaLigaScorersEmbedService
    {
        private readonly ILogger<LaLigaScorersEmbedService> _logger;
        private readonly ICountryEmojiService _emojiService;

        public LaLigaScorersEmbedService(ILogger<LaLigaScorersEmbedService> logger, ICountryEmojiService emojiService)
        {
            _logger = logger;
            _emojiService = emojiService;
        }

        public EmbedBuilder CreateScorersEmbed(IList<Scorer> scorers)
        {
            var builder = new EmbedBuilder()
                .WithTitle("LaLiga Current Top Scorers")
                .WithColor(Color.Orange)
                .WithThumbnailUrl(
                    "https://assets.laliga.com/assets/logos/laliga-v-negativo/laliga-v-negativo-1200x1200.jpg")
                .WithFooter($"Stats updated at {scorers.First().UpdatedAt:dd/MM/yyyy HH:mm} UTC");

            foreach (var scorer in scorers)
            {
                builder.AddField($"{scorer.Id + 1}. {scorer.Name}, {scorer.Goals} goals",
                    $"{_emojiService.GetEmojiString(scorer.Nationality)}, {scorer.Team.Name}");
            }

            return builder;
        }
    }
}