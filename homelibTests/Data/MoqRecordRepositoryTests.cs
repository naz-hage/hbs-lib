using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using homelibTests;
using Moq.EntityFrameworkCore;
using homelib.Entities;
using homelib.Data;

namespace homelibTests.Data
{
    [TestClass]
    public class MoqRecordRepositoryTests
    {
        private static List<Record> GetTestRecords()
        {
            return new List<Record>
            {
                new() { Name = "The Name", Value="The Value" },
                new() { Name = "The second name", Value ="The value of the second name" },
                // Add more records as needed
            };
        }

        [TestMethod]
        public async Task GetAllRecordsAsync_ReturnsAllRecords()
        {
            // Arrange
            var data = GetTestRecords();
            var mockSet = data.AsDbSetMock();

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Records).Returns(mockSet.Object);

            var repository = new RecordRepository(mockContext.Object);

            // Act
            var records = await repository.GetAllRecordsAsync();

            // Assert
            Assert.AreEqual(2, records.Count); // Change the number to match the number of records in GetTestRecords

            // Assert that the records returned are the same as the records in GetTestRecords
            Assert.AreEqual(data[0].Name, records[0].Name);
            Assert.AreEqual(data[0].Value, records[0].Value);
            Assert.AreEqual(data[1].Name, records[1].Name);
            Assert.AreEqual(data[1].Value, records[1].Value);
        }
    }
}