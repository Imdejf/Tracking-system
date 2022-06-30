using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrackingSystem.Persistence.DataAccess.EntitiesConfig.Identity
{
    public sealed class UserConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User", "identity");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            builder.HasMany(c => c.UserPermissions);

            builder.Property(c => c.UserName)
                   .HasColumnType("varchar")
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(c => c.NormalizedUserName)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired(false);

            builder.Property(c => c.FirstName)
                   .HasColumnType("varchar")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.LastName)
                   .HasColumnType("varchar")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.Email)
                   .HasColumnType("varchar")
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(c => c.NormalizedEmail)
                   .HasColumnType("varchar")
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(c => c.PhoneNumber)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(c => c.PhoneNumberConfirmed)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(c => c.PasswordHash)
                   .HasColumnType("varchar(max)")
                   .IsRequired();

            builder.Property(c => c.SecurityStamp)
                   .HasColumnType("varchar(max)");

            builder.Property(c => c.ConcurrencyStamp)
                   .HasColumnType("varchar(max)");

            builder.Property(c => c.RegisterSource)
                   .HasColumnType("varchar")
                   .HasMaxLength(50)
                   .IsRequired()
                   .HasConversion(
                   x => x.ToString(),
                   x => (UserRegisterSource)Enum.Parse(typeof(UserRegisterSource), x, true));

            builder.Property(c => c.CreatedDate)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(c => c.LastModifiedDate)
                   .HasColumnType("datetime")
                   .IsRequired(false);
        }
    }
}
