using Microsoft.EntityFrameworkCore;

namespace DSV_Book_a_room.Models
{
    public class DsvDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("DSV-Db");
        }
    }
}
