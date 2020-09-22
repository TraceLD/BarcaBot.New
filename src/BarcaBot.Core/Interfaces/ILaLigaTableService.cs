using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Models.Table;

namespace BarcaBot.Core.Interfaces
{
    public interface ILaLigaTableService
    {
        Task<IList<TablePosition>> GetAsync();
        Task<TablePosition> GetAsync(int position);
        Task UpsertAsync(TablePosition tablePositionIn);
    }
}