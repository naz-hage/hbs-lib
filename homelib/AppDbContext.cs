using Microsoft.EntityFrameworkCore;

namespace homelib
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
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}