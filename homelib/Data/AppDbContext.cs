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
            if (Environment.GetEnvironmentVariable("USE_IN_MEMORY_DATABASE") == "True")
            {
                optionsBuilder.UseInMemoryDatabase("TestDatabase");
            }
            else if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=C:\sqlite\data.db");
            }
        }
    }
}