using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.UserId)
                .IsRequired();

            builder.Property(b => b.RestaurantId)
                .IsRequired();

            builder.Property(b => b.Type)
                .IsRequired();

            builder.Property(b => b.Description)
                .IsRequired();
        }
    }
}
