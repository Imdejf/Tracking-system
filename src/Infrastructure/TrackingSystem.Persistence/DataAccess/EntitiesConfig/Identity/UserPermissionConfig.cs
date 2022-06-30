using TrackingSystem.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrackingSystem.Persistence.DataAccess.EntitiesConfig.Identity
{
    public sealed class UserPermissionConfig : IEntityTypeConfiguration<UserPermissionEntity>
    {
        public void Configure(EntityTypeBuilder<UserPermissionEntity> builder)
        {
            builder.ToTable("UserPermission", "identity");

            builder.HasKey(c => new
            {
                c.PermissionDomainName,
                c.PermissionFlagValue,
                c.UserId
            });


            builder.HasOne(c => c.User)
                   .WithMany(c => c.UserPermissions)
                   .HasForeignKey(c => c.UserId);

            builder.Property(c => c.PermissionFlagValue)
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(c => c.PermissionDomainName)
                   .HasColumnType("varchar")
                   .HasMaxLength(200)
                   .IsRequired();
        }
    }
}
