using System;
using System.Net.Http;

using Microsoft.Extensions.Options;

using BarcaBot.DataModels.Core;

namespace BarcaBot.Services.Http
{
    public class FootballDataService
    {
        private readonly IOptions<Settings> _settings;
        private readonly HttpClient _client;

        public FootballDataService(IOptions<Settings> settings, HttpClient client)
        {
            _settings = settings;
            
            client.BaseAddress = new Uri("https://api.football-data.org/v2/");
            client.DefaultRequestHeaders.Add("X-Auth-Token", settings.Value.Apis.FootballData.Token);

            _client = client;
        }
    }
}