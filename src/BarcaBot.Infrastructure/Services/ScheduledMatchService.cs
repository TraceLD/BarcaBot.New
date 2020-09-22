using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BarcaBot.Infrastructure.Services
{
    public class ScheduledMatchService : IScheduledMatchService
    {
        private readonly ILogger<ScheduledMatchService> _logger;
        private readonly IMongoCollection<ScheduledMatch> _collection;

        public ScheduledMatchService(ILogger<ScheduledMatchService> logger, IMongoDatabase database)
        {
            _logger = logger;
            _collection = database.GetCollection<ScheduledMatch>("scheduledmatches");
        }

        public async Task<IList<ScheduledMatch>> GetAsync()
            => await _collection.Find(match => true).ToListAsync();

        public async Task<ScheduledMatch> GetAsync(int id)
            => await _collection.Find(match => match.Id == id).FirstOrDefaultAsync();

        public async Task UpsertAsync(ScheduledMatch matchIn)
        {
            var options = new ReplaceOptions{IsUpsert = true};
            await _collection.ReplaceOneAsync(
                match => match.Id == matchIn.Id, matchIn, options);
        }
    }
}