using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BarcaBot.Core.Models.Dto.ApiFootball
{
    public class PlayersResponseDto
    {
        [JsonPropertyName("api")]
        public ApiValueDto Value { get; set; }
    }
    
    public class ApiValueDto
    {
        public int Results { get; set; }
        
        public IEnumerable<PlayerDto> Players { get; set; }
    }
}