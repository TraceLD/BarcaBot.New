using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BarcaBot.Infrastructure.Services
{
    public class LaLigaScorersService : ILaLigaScorersService
    {
        private readonly ILogger<LaLigaScorersService> _logger;
        private readonly IMongoCollection<Scorer> _collection;

        public LaLigaScorersService(ILogger<LaLigaScorersService> logger, IMongoDatabase database)
        {
            _logger = logger;
            _collection = database.GetCollection<Scorer>("leaguescorers");
        }

        public async Task<IList<Scorer>> GetAsync()
            => await _collection.Find(scorer => true).ToListAsync();

        public async Task UpsertAsync(Scorer scorerIn)
        {
            var options = new ReplaceOptions{IsUpsert = true};
            await _collection.ReplaceOneAsync(
                scorer => scorer.Id == scorerIn.Id, scorerIn, options);
        }
    }
}