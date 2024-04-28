using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<MenuEntity>
    {
        public const int MAX_NAME_LENGTH = 50;
        public const int MAX_DESCRIPTION_LENGTH = 200;

        public void Configure(EntityTypeBuilder<MenuEntity> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.RestaurantId).IsRequired();
            builder.Property(m => m.Price).IsRequired();

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(MAX_NAME_LENGTH);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(MAX_DESCRIPTION_LENGTH);

            builder.Property(m => m.Photo)
                .IsRequired();
        }
    }
}
