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
            builder.ToTable("User");

            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.FirstName)
                   .IsRequired(true)
                   .HasMaxLength(300);

            builder.Property(c => c.LastName)
                   .IsRequired(true)
                   .HasMaxLength(300);

            builder.HasMany(c => c.UserPermissions)
                   .WithOne(c => c.User)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
