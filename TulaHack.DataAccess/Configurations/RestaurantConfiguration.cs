using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<RestaurantEntity>
    {
        public const int MAX_STRING_LENGTH = 50;

        public void Configure(EntityTypeBuilder<RestaurantEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(MAX_STRING_LENGTH);

            builder.Property(r => r.Subtitle)
                .IsRequired()
                .HasMaxLength(MAX_STRING_LENGTH);

            builder.Property(r => r.Description)
                .IsRequired();

            builder.Property(r => r.UserId)
                .IsRequired();

            builder
                .HasOne(r => r.User);

            builder.Property(r => r.Address)
                .IsRequired()
                .HasMaxLength(MAX_STRING_LENGTH);

            builder.Property(r => r.StartWorkTime)
                .IsRequired();

            builder.Property(r => r.EndWorkTime)
                .IsRequired();
        }
    }
}
