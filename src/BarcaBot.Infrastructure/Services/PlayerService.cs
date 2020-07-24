using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Player;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BarcaBot.Infrastructure.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ILogger<PlayerService> _logger;
        private readonly IMongoCollection<Player> _players;

        public PlayerService(ILogger<PlayerService> logger, IMongoDatabase database)
        {
            _logger = logger;
            _players = database.GetCollection<Player>("players");
        }

        public async Task<IList<Player>> GetAsync()
            => await _players.Find(player => true).ToListAsync();

        public async Task<Player> GetAsync(int id)
            => await _players.Find(player => player.Id == id).FirstOrDefaultAsync();

        public async Task<Player> GetAsync(string name)
            => await _players.Find(player => player.Name.ToLower().Contains(name)).FirstOrDefaultAsync();

        public async Task UpsertAsync(Player playerIn)
        {
            var options = new ReplaceOptions{IsUpsert = true};
            await _players.ReplaceOneAsync(player => player.Id == playerIn.Id, playerIn, options);
        }
    }
}