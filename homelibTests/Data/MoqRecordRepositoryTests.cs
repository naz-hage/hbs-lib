using homelib.Data;
using homelib.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
        [TestMethod]
        public async Task AddRecordAsync_AddsRecord()
        {
            // Arrange
            var data = GetTestRecords();
            var mockSet = data.AsDbSetMock();

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Records).Returns(mockSet.Object);

            var repository = new RecordRepository(mockContext.Object);

            // Act
            await repository.AddRecordAsync(new Record { Name = "The third name", Value = "The value of the third name" });

            // Assert
            mockSet.Verify(x => x.Add(It.IsAny<Record>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task DeleteRecordAsync_DeletesRecord()
        {
            // Arrange
            var data = GetTestRecords();
            var mockSet = data.AsDbSetMock();

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Records).Returns(mockSet.Object);

            var repository = new RecordRepository(mockContext.Object);

            // Act
            await repository.DeleteRecordAsync(data[0]);

            // Assert
            mockSet.Verify(x => x.Remove(It.IsAny<Record>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task UpdateRecordAsync_UpdatesRecord()
        {
            // Arrange
            var data = GetTestRecords();
            var mockSet = data.AsDbSetMock();

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Records).Returns(mockSet.Object);

            var repository = new RecordRepository(mockContext.Object);

            // Act
            await repository.UpdateRecordAsync(data[0]);

            // Assert
            mockSet.Verify(x => x.Update(It.IsAny<Record>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
        [TestMethod, Ignore("This test requires a lot of code to implement IAsyncQueryProvider, " +
            "which is required for Entity Framework's async operations.  The test in the InMemoryRecordRepositoryTests" +
            "is sufficient")]
        public async Task GetRecordByIdAsync_ReturnsRecord()
        {
            // Arrange
            var data = GetTestRecords();
            var mockSet = data.AsDbSetMock();

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Records).Returns(mockSet.Object);
            
            var repository = new RecordRepository(mockContext.Object);

            Record record;
            // Act
            try
            {
                record = await repository.GetRecordByIdAsync(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            

            // Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(data[0].Name, record.Name);
            Assert.AreEqual(data[0].Value, record.Value);
        }
    }
}