using homelib.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace homelib.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Record> Records { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Parameterless constructor for Moq testing
        protected AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var databaseType = Environment.GetEnvironmentVariable("HBS_HOMELIB_DATABASE_TYPE");

            if (databaseType == "IN_MEMORY")
            {
                optionsBuilder.UseInMemoryDatabase("TestDatabase");
            }
            else if (databaseType == "Sqlite")
            {
                var databaseLocation = Environment.GetEnvironmentVariable("HBS_HOMELIB_DATABASE_LOCATION");
                if (string.IsNullOrEmpty(databaseLocation))
                {
                    throw new Exception("HBS_HOMELIB_DATABASE_LOCATION environment variable is not set");
                }
                optionsBuilder.UseSqlite($"Data Source={databaseLocation}");
            }
            else
            {
                throw new Exception("HBS_HOMELIB_DATABASE_TYPE environment variable is not set");
            }
        }
    }
}