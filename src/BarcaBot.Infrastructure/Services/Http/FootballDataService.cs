using System;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces.Http;
using BarcaBot.Core.Models.FootballData.Matches;
using BarcaBot.Core.Models.FootballData.Table;
using BarcaBot.Core.Models.Settings;

namespace BarcaBot.Infrastructure.Services.Http
{
    public class FootballDataService : IFootballDataService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        
        private const int LaLigaId = 2014;
        private const int FcBarcelonaId = 81;
        
        public FootballDataService(ApisSettings settings, HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.football-data.org/v2/");
            client.DefaultRequestHeaders.Add("X-Auth-Token", settings.FootballData.Token);

            _client = client;
            
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.PropertyNameCaseInsensitive = true;
            _serializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
        }

        public async Task<StandingsResponse> GetLaLigaStandings()
        {
            var response = await _client.GetAsync(
            $"competitions/{LaLigaId}/standings");
            
            response.EnsureSuccessStatusCode();
            
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseDeserialized = await JsonSerializer.DeserializeAsync
                <StandingsResponse>(responseStream, _serializerOptions);
            return responseDeserialized;
        }

        public async Task<MatchesResponse> GetScheduledMatches()
        {
            var response = await _client.GetAsync(
                $"teams/{FcBarcelonaId}/matches?status=SCHEDULED");

            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseDeserialized = await JsonSerializer.DeserializeAsync
                <MatchesResponse>(responseStream, _serializerOptions);
            return responseDeserialized;
        }
    }
}