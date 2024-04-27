using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<BookingEntity>
    {
        public void Configure(EntityTypeBuilder<BookingEntity> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.UserId).IsRequired();
            builder.Property(b => b.RestaurantId).IsRequired();
            builder.Property(b => b.TableId).IsRequired();
            builder.Property(b => b.Date).IsRequired();
            builder.Property(b => b.Date).IsRequired();
            builder.Property(b => b.StartTime).IsRequired();
            builder.Property(b => b.EndTime).IsRequired();
            builder.Property(b => b.PersonsNumber).IsRequired();
            builder.Property(b => b.Status).IsRequired();
        }
    }
}
