using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Dto.FootballData;
using BarcaBot.Core.Models.Settings;
using BarcaBot.Core.Models.Table;
using Microsoft.Extensions.Options;

namespace BarcaBot.Infrastructure.Services.Http
{
    public class FootballDataService : IFootballDataService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        
        private const int LaLigaId = 2014;
        
        public FootballDataService(ApisSettings settings, HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.football-data.org/v2/");
            client.DefaultRequestHeaders.Add("X-Auth-Token", settings.FootballData.Token);

            _client = client;
            
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.PropertyNameCaseInsensitive = true;
            _serializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
        }

        public async Task<StandingsResponseDto> GetLaLigaStandings()
        {
            var response = await _client.GetAsync(
            $"competitions/2014/standings");
            response.EnsureSuccessStatusCode();
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseDeserialized = await JsonSerializer.DeserializeAsync
                <StandingsResponseDto>(responseStream, _serializerOptions);
            return responseDeserialized;
        }
    }
}