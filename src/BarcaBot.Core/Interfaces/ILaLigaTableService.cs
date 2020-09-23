using System.Collections.Generic;
using System.Threading.Tasks;
using BarcaBot.Core.Models.Table;

namespace BarcaBot.Core.Interfaces
{
    public interface ILaLigaTableService
    {
        Task<IList<TablePosition>> GetAsync();
        Task<TablePosition> GetAsync(int position);
        Task<IList<TablePosition>> GetTop5();
        Task<IList<TablePosition>> GetBottom5();
        Task UpsertAsync(TablePosition tablePositionIn);
    }
}