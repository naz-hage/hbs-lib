using homelib.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homelib.Interfaces
{
    public interface IRecordRepository
    {
        Task<List<Record>> GetAllRecordsAsync();
        Task AddRecordAsync(Record record);
        Task DeleteRecordAsync(Record record);
        Task UpdateRecordAsync(Record record);
    }
}