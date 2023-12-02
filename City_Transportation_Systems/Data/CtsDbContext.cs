using Microsoft.EntityFrameworkCore;
using City_Transportation_Systems.Models;

namespace City_Transportation_Systems.Data
{
    public class CtsDbContext : DbContext
    {
        public CtsDbContext()
        {
               
        }
        public CtsDbContext(DbContextOptions<CtsDbContext> options) : base(options)
        {
        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CTS_DB;Trusted_Connection=True;TrustServerCertificate=true;");
        }

    }
}
