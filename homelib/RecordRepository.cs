using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homelib
{
    public class RecordRepository
    {
        private readonly AppDbContext _context;

        public RecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Record>> GetAllRecordsAsync()
        {
            return await _context.Records.ToListAsync();
        }

        public async Task AddRecordAsync(Record record)
        {
            _context.Records.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecordAsync(Record record)
        {
            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecordAsync(Record record)
        {
            _context.Records.Update(record);
            await _context.SaveChangesAsync();
        }

    }
}