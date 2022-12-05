using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingSystem.Domain.Entities.Truck;

namespace JustCommerce.Persistence.DataAccess.EntitiesConfig.Truck
{
    internal sealed class TruckTypeConfig : IEntityTypeConfiguration<TruckEntity>
    {
        public void Configure(EntityTypeBuilder<TruckEntity> builder)
        {
            builder.ToTable("Truck");

            builder.HasKey(c => c.TruckId);
            builder.HasIndex(c => c.TruckId);

            builder.HasOne(c => c.TruckDetails)
                   .WithOne(c => c.Truck)
                   .HasForeignKey<TruckDetailsEntity>(c => c.TruckId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.UserTrucks)
                   .WithOne(c => c.Truck)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
