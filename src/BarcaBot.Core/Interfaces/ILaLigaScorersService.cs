using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Models;

namespace BarcaBot.Core.Interfaces
{
    public interface ILaLigaScorersService
    {
        Task<IList<Scorer>> GetAsync();
        Task UpsertAsync(Scorer scorerIn);
    }
}