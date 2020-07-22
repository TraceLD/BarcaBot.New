using System;
using System.Net.Http;
using BarcaBot.Core.Models.Settings;
using Microsoft.Extensions.Options;

namespace BarcaBot.Infrastructure.Services.Http
{
    public class FootballDataService
    {
        private readonly ApisSettings _settings;
        private readonly HttpClient _client;

        public FootballDataService(ApisSettings settings, HttpClient client)
        {
            _settings = settings;
            
            client.BaseAddress = new Uri("https://api.football-data.org/v2/");
            client.DefaultRequestHeaders.Add("X-Auth-Token", settings.FootballData.Token);

            _client = client;
        }
    }
}