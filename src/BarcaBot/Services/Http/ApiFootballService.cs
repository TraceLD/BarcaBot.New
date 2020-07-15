using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using BarcaBot.DataModels.ApiFootball;
using BarcaBot.DataModels.Core;
using BarcaBot.Json;

namespace BarcaBot.Services.Http
{
    public class ApiFootballService
    {
        private readonly IOptions<Settings> _settings;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        
        public ApiFootballService(IOptions<Settings> settings, HttpClient client)
        {
            _settings = settings;
            
            client.BaseAddress = new Uri("https://api-football-v1.p.rapidapi.com/v2/");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", settings.Value.Apis.ApiFootball.Token);

            _client = client;
            
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.PropertyNameCaseInsensitive = true;
            _serializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse());
            _serializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            var response = await _client.GetAsync(
                $"players/team/529/{_settings.Value.Apis.ApiFootball.Season}");
            
            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseDeserialized = await JsonSerializer.DeserializeAsync
                <PlayersResponse>(responseStream, _serializerOptions);

            return responseDeserialized.Value.Players;
        }
    }
}