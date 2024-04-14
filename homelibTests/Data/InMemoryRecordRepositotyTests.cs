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
    }
}
