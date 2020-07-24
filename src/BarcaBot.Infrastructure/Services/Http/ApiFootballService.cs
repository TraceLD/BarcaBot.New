using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Json;
using BarcaBot.Core.Models.Dto.ApiFootball;
using BarcaBot.Core.Models.Settings;

namespace BarcaBot.Infrastructure.Services.Http
{
    public class ApiFootballService : IApiFootballService
    {
        private readonly ApisSettings _settings;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        
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

        public async Task<IEnumerable<PlayerDto>> GetPlayerDtosAsync()
        {
            var response = await _client.GetAsync(
                $"players/team/529/{_settings.ApiFootball.Season}");
            
            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseDeserialized = await JsonSerializer.DeserializeAsync
                <PlayersResponseDto>(responseStream, _serializerOptions);

            return responseDeserialized.Value.Players;
        }
    }
}