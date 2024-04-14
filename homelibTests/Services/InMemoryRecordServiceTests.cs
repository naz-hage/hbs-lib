using homelib.Data;
using homelib.Entities;
using homelib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace homelibTests.Services
{
    [TestClass]
    public class RecordServiceTests
    {
        private RecordService? _recordService;
        private AppDbContext? _context;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _recordService = new RecordService(new RecordRepository(_context));
        }

        [TestMethod]
        public async Task GetAllRecordsAsync_ReturnsAllRecords()
        {
            // Arrange
            var records = new List<Record> { new(), new() };
            Assert.IsNotNull(_context);
            Assert.IsNotNull(_recordService);
            _context.Records.AddRange(records);
            await _context.SaveChangesAsync();

            // Act
            var result = await _recordService.GetAllRecordsAsync();

            // Assert
            Assert.AreEqual(records.Count, result.Count);
        }

        // Add more tests for AddRecordAsync, DeleteRecordAsync, UpdateRecordAsync...
        [TestMethod]
        public async Task AddRecordAsync_AddsRecord()
        {
            // Arrange
            var record = new Record { Name = "The Name", Value = "The Value" };
            Assert.IsNotNull(_recordService);
            Assert.IsNotNull(_context);

            // Act
            await _recordService.AddRecordAsync(record);

            // Assert
            var result = await _context.Records.FirstOrDefaultAsync(r => r.Name == "The Name");
            Assert.IsNotNull(result);
            Assert.AreEqual("The Value", result.Value);
        }

        [TestMethod]
        public async Task DeleteRecordAsync_DeletesRecord()
        {
            // Arrange
            var record = new Record { Name = "The Name", Value = "The Value" };
            Assert.IsNotNull(_recordService);
            Assert.IsNotNull(_context);
            _context.Records.Add(record);
            await _context.SaveChangesAsync();

            // Act
            await _recordService.DeleteRecordAsync(record);

            // Assert
            var result = await _context.Records.FirstOrDefaultAsync(r => r.Name == "The Name");
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateRecordAsync_UpdatesRecord()
        {
            // Arrange
            var record = new Record { Name = "The Name", Value = "The Value" };
            Assert.IsNotNull(_recordService);
            Assert.IsNotNull(_context);

            _context.Records.Add(record);
            await _context.SaveChangesAsync();

            // Act
            record.Value = "The New Value";
            await _recordService.UpdateRecordAsync(record);

            // Assert
            var result = await _context.Records.FirstOrDefaultAsync(r => r.Name == "The Name");
            Assert.AreEqual("The New Value", result?.Value);
        }
    }
}
