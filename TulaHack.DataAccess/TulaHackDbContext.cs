using Microsoft.EntityFrameworkCore;
using TulaHack.DataAccess.Configurations;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess
{
    public class TulaHackDbContext : DbContext
    {
        public TulaHackDbContext(DbContextOptions<TulaHackDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RestaurantEntity> Restaurants { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<SchemeEntity> Schemes { get; set; }
        public DbSet<TableEntity> Tables { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new SchemeConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
