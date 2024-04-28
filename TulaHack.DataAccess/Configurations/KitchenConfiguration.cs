using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Configurations
{
    public class KitchenConfiguration : IEntityTypeConfiguration<KitchenEntity>
    {
        public void Configure(EntityTypeBuilder<KitchenEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(k => k.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
