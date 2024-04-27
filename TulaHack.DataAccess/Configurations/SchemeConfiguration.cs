using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Configurations
{
    public class SchemeConfiguration : IEntityTypeConfiguration<SchemeEntity>
    {
        public void Configure(EntityTypeBuilder<SchemeEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.RestaurantId).IsRequired();
        }
    }
}
