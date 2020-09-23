using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Table;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BarcaBot.Infrastructure.Services
{
    public class LaLigaTableService : ILaLigaTableService
    {
        private readonly ILogger<LaLigaTableService> _logger;
        private readonly IMongoCollection<TablePosition> _table;

        public LaLigaTableService(ILogger<LaLigaTableService> logger, IMongoDatabase database)
        {
            _logger = logger;
            _table = database.GetCollection<TablePosition>("leaguetable");
        }

        public async Task<IList<TablePosition>> GetAsync()
            => await _table.Find(tablePosition => true).ToListAsync();

        public async Task<TablePosition> GetAsync(int position)
            => await _table.Find(tablePosition => tablePosition.Id == position).FirstOrDefaultAsync();

        public async Task<IList<TablePosition>> GetTop5()
            => await _table.Find(tablePosition => tablePosition.Id <= 5).ToListAsync();

        public async Task<IList<TablePosition>> GetBottom5()
            => await _table.Find(tablePosition => tablePosition.Id >= 16).ToListAsync();

        public async Task UpsertAsync(TablePosition tablePositionIn)
        {
            var options = new ReplaceOptions{IsUpsert = true};
            await _table.ReplaceOneAsync(
                tablePosition => tablePosition.Id == tablePositionIn.Id, tablePositionIn,
                options);
        }
    }
}