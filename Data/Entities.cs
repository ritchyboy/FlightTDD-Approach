using Domains;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class Entities:DbContext
    {
        public DbSet<Flight> Flights => Set<Flight>();
        public Entities(DbContextOptions option):base(option) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasKey(f => f.Id);
            modelBuilder.Entity<Flight>().OwnsMany(f => f.BookingList);

            base.OnModelCreating(modelBuilder);
        }
    }
}
