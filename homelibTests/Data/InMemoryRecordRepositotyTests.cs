using homelib.Data;
using homelib.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace homelibTests.Data
{

    [TestClass]
    public class InMemoryRecordRepositotyTests
    {
        private DbContextOptions<AppDbContext>? _options;

        [TestInitialize]
        public void TestInitialize()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Using GUID to ensure that each test runs with a new InMemory database.
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            // Ensure the database is created.
            using var context = new AppDbContext(_options);
            context.Database.EnsureCreated();
        }

        [TestMethod]
        public async Task GetAllRecordsAsync_ReturnsAllRecords()
        {
            // Arrange
            Assert.IsNotNull(_options);
            using (var context = new AppDbContext(_options))
            {
                context.Records.Add(new Record { Name = "The Name", Value = "The Value" });
                await context.SaveChangesAsync();
            }

            // Act
            List<Record> records;
            using (var context = new AppDbContext(_options))
            {
                var repository = new RecordRepository(context);
                records = await repository.GetAllRecordsAsync();
            }

            // Assert
            Assert.AreEqual(1, records.Count);

            Assert.AreEqual("The Name", records[0].Name);
            Assert.AreEqual("The Value", records[0].Value);
        }

        [TestMethod]
        public async Task AddRecordAsync_AddsRecord()
        {
            // Arrange
            Assert.IsNotNull(_options);
            using (var context = new AppDbContext(_options))
            {
                context.Records.Add(new Record { Name = "The Name", Value = "The Value" });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new AppDbContext(_options))
            {
                var repository = new RecordRepository(context);
                await repository.AddRecordAsync(new Record { Name = "The Name 2", Value = "The Value 2" });
            }

            // Assert
            using (var context = new AppDbContext(_options))
            {
                var records = await context.Records.ToListAsync();
                Assert.AreEqual(2, records.Count);
                Assert.AreEqual("The Name", records[0].Name);
                Assert.AreEqual("The Value", records[0].Value);
                Assert.AreEqual("The Name 2", records[1].Name);
                Assert.AreEqual("The Value 2", records[1].Value);
            }
        }

        [TestMethod]
        public async Task DeleteRecordAsync_DeletesRecord()
        {
            // Arrange
            Assert.IsNotNull(_options);
            using (var context = new AppDbContext(_options))
            {
                context.Records.Add(new Record { Name = "The Name", Value = "The Value" });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new AppDbContext(_options))
            {
                var repository = new RecordRepository(context);
                var record = await context.Records.FirstAsync();
                await repository.DeleteRecordAsync(record);
            }

            // Assert
            using (var context = new AppDbContext(_options))
            {
                var records = await context.Records.ToListAsync();
                Assert.AreEqual(0, records.Count);
            }
        }

        [TestMethod]
        public async Task UpdateRecordAsync_UpdatesRecord()
        {
            // Arrange
            Assert.IsNotNull(_options);
            using (var context = new AppDbContext(_options))
            {
                context.Records.Add(new Record { Name = "The Name", Value = "The Value" });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new AppDbContext(_options))
            {
                var repository = new RecordRepository(context);
                var record = await context.Records.FirstAsync();
                record.Name = "The Name 2";
                record.Value = "The Value 2";
                await repository.UpdateRecordAsync(record);
            }

            // Assert
            using (var context = new AppDbContext(_options))
            {
                var records = await context.Records.ToListAsync();
                Assert.AreEqual(1, records.Count);
                Assert.AreEqual("The Name 2", records[0].Name);
                Assert.AreEqual("The Value 2", records[0].Value);
            }
        }

        [TestMethod]
        public async Task GetRecordByIdAsync_ReturnsRecord()
        {
            // Arrange
            Assert.IsNotNull(_options);
            using (var context = new AppDbContext(_options))
            {
                context.Records.Add(new Record { Name = "The Name-random", Value = "The Value-nonRandon" });
                await context.SaveChangesAsync();
            }

            // Act
            Record record;
            using (var context = new AppDbContext(_options))
            {
                var repository = new RecordRepository(context);
                record = await repository.GetRecordByIdAsync(1);
            }

            // Assert
            Assert.IsNotNull(record);
            Assert.AreEqual("The Name-random", record.Name);
            Assert.AreEqual("The Value-nonRandon", record.Value);
        }

        [TestMethod]
        public async Task GetRecordByIdAsync_ReturnsNull()
        {
            // Arrange
            Assert.IsNotNull(_options);
            using (var context = new AppDbContext(_options))
            {
                context.Records.Add(new Record { Name = "The Name-random", Value = "The Value-nonRandon" });
                await context.SaveChangesAsync();
            }

            // Act
            Record record;
            using (var context = new AppDbContext(_options))
            {
                var repository = new RecordRepository(context);
                record = await repository.GetRecordByIdAsync(1000);
            }

            // Assert
            Assert.IsNull(record);
        }
    }
}
