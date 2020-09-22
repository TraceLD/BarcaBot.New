using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces.Http;
using BarcaBot.Core.Models.ApiFootball.Players;
using BarcaBot.Core.Models.Settings;

namespace BarcaBot.Infrastructure.Services.Http
{
    public class ApiFootballService : IApiFootballService
    {
        private readonly ApisSettings _settings;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        private const int FcBarcelonaTeamId = 529;
        
        public ApiFootballService(ApisSettings settings, HttpClient client)
        {
            _settings = settings;
            
            client.BaseAddress = new Uri("https://api-football-v1.p.rapidapi.com/v2/");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", settings.ApiFootball.Token);

            _client = client;
            
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.PropertyNameCaseInsensitive = true;
            _serializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            _serializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }

        public async Task<IEnumerable<Player>> GetPlayerDtosAsync()
        {
            var response = await _client.GetAsync(
                $"players/team/{FcBarcelonaTeamId}/{_settings.ApiFootball.Season}");
            response.EnsureSuccessStatusCode();
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseDeserialized = await JsonSerializer.DeserializeAsync
                <PlayersResponse>(responseStream, _serializerOptions);
            return responseDeserialized.Value.Players;
        }
    }
}