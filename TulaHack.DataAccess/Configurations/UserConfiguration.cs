using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public const int MAX_STRING_LENGTH = 50;
        public const int MAX_PASSWORD_LENGTH = 255;

        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(MAX_STRING_LENGTH);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(MAX_STRING_LENGTH);

            builder.Property(u => u.MiddleName)
                .IsRequired()
                .HasMaxLength(MAX_STRING_LENGTH);

            builder.Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(MAX_STRING_LENGTH);

            builder.Property(u => u.Role)
                .IsRequired();

            builder.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(MAX_STRING_LENGTH);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(MAX_PASSWORD_LENGTH);
        }
    }
}
