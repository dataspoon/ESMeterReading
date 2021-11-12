using Microsoft.EntityFrameworkCore;

namespace EnsekTechnicalExercise.Api.Data
{
    public class MeterContext : DbContext
    {
        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public string DbPath { get; private set; }

        public MeterContext()
        {            
            DbPath = $"Database/MeterReadings.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}