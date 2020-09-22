using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Models;

namespace BarcaBot.Core.Interfaces
{
    public interface IScheduledMatchService
    {
        Task<IList<ScheduledMatch>> GetAsync();
        Task<ScheduledMatch> GetAsync(int id);
        Task UpsertAsync(ScheduledMatch matchIn);
    }
}