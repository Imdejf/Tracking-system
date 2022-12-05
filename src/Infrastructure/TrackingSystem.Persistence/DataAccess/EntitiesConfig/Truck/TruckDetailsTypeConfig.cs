using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingSystem.Domain.Entities.Truck;

namespace JustCommerce.Persistence.DataAccess.EntitiesConfig.Truck
{
    internal sealed class TruckDetailsTypeConfig : IEntityTypeConfiguration<TruckDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<TruckDetailsEntity> builder)
        {
            builder.ToTable("TruckDetails");

            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id);

            builder.HasOne(c => c.Truck)
                   .WithOne(c => c.TruckDetails)
                   .HasForeignKey<TruckDetailsEntity>(c => c.TruckId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
