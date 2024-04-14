using homelib.Data;
using homelib.Entities;
using homelib.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homelib.Services
{
    public class RecordService(IRecordRepository recordRepository)
    {
        private readonly IRecordRepository _recordRepository = recordRepository;

        public async Task<List<Record>> GetAllRecordsAsync()
        {
            return await _recordRepository.GetAllRecordsAsync();
        }

        public async Task AddRecordAsync(Record record)
        {
            await _recordRepository.AddRecordAsync(record);
        }

        public async Task DeleteRecordAsync(Record record)
        {
            await _recordRepository.DeleteRecordAsync(record);
        }

        public async Task UpdateRecordAsync(Record record)
        {
            await _recordRepository.UpdateRecordAsync(record);
        }
    }
}