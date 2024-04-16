using homelib.Data;
using homelib.Entities;
using homelib.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homelib.Services
{
    public class RecordService(IRecordRepository recordRepository)
    {
        private readonly IRecordRepository _recordRepository = recordRepository;

        public async Task<List<Record>> GetAllRecordsAsync()
        {
            var records = await _recordRepository.GetAllRecordsAsync();
            return records ?? [];  // Return an empty list if records is null
        }

        public async Task AddRecordAsync(Record record)
        {
            try
            {
                await _recordRepository.AddRecordAsync(record);
            }
            catch (Exception ex)
            {
                // Log the error or perform error handling logic
                Console.WriteLine($"Error adding record: {ex.Message}");

                // Rethrow the exception to propagate it to the caller
                throw;
            }
        }

        public async Task DeleteRecordAsync(Record record)
        {
            try
            {
                await _recordRepository.DeleteRecordAsync(record);
            }
            catch (Exception ex)
            {
                // Log the error or perform error handling logic
                Console.WriteLine($"Error deleting record: {ex.Message}");

                // Rethrow the exception to propagate it to the caller
                throw;
            }
        }

        public async Task UpdateRecordAsync(Record record)
        {
            try
            {
                await _recordRepository.UpdateRecordAsync(record);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating record: {ex.Message}");

                // Rethrow the exception to propagate it to the caller
                throw;
            }
        }
    }
}