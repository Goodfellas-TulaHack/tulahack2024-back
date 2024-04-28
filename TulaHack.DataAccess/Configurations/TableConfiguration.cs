using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<TableEntity>
    {
        public void Configure(EntityTypeBuilder<TableEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.SchemeId).IsRequired();
            builder.Property(t => t.Persons).IsRequired();
            builder.Property(t => t.X).IsRequired();
            builder.Property(t => t.Y).IsRequired();
            builder.Property(t => t.Width).IsRequired();
            builder.Property(t => t.Height).IsRequired();
            builder.Property(t => t.Type).IsRequired();
            builder.Property(t => t.Fill).IsRequired();
        }
    }
}
