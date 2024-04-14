using Microsoft.VisualStudio.TestTools.UnitTesting;
using homelib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using homelib.Data;
using homelib.Entities;
using Moq;
using homelib.Interfaces;

namespace homelib.Services.Tests
{
    [TestClass]
    public class MoqRecordServiceTests
    {
        private Mock<IRecordRepository>? _mockRecordRepository;
        private RecordService? _recordService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRecordRepository = new Mock<IRecordRepository>();
            _recordService = new RecordService(_mockRecordRepository.Object);
        }

        [TestMethod]
        public async Task GetAllRecordsAsync_ReturnsAllRecords()
        {
            // Arrange
            var records = new List<Record> { new(), new() };
            _mockRecordRepository?.Setup(repo => repo.GetAllRecordsAsync()).ReturnsAsync(records);

            Assert.IsNotNull(_recordService);

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
            Assert.IsNotNull(_mockRecordRepository);

            // Act
            await _recordService.AddRecordAsync(record);

            // Assert
            _mockRecordRepository.Verify(repo => repo.AddRecordAsync(record), Times.Once);
        }

        [TestMethod]
        public async Task DeleteRecordAsync_DeletesRecord()
        {
            // Arrange
            var record = new Record { Name = "The Name", Value = "The Value" };
            Assert.IsNotNull(_recordService);
            Assert.IsNotNull(_mockRecordRepository);

            // Act
            await _recordService.DeleteRecordAsync(record);

            // Assert
            _mockRecordRepository.Verify(repo => repo.DeleteRecordAsync(record), Times.Once);
        }

        [TestMethod]
        public async Task UpdateRecordAsync_UpdatesRecord()
        {
            // Arrange
            var record = new Record { Name = "The Name", Value = "The Value" };
            Assert.IsNotNull(_recordService);
            Assert.IsNotNull(_mockRecordRepository);

            // Act
            await _recordService.UpdateRecordAsync(record);

            // Assert
            _mockRecordRepository.Verify(repo => repo.UpdateRecordAsync(record), Times.Once);
        }
    }
}