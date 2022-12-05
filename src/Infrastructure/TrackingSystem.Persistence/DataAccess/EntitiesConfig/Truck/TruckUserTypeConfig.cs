using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystem.Domain.Entities.Truck;

namespace JustCommerce.Persistence.DataAccess.EntitiesConfig.Truck
{
    internal sealed class TruckUserTypeConfig : IEntityTypeConfiguration<UserTruckEntity>
    {
        public void Configure(EntityTypeBuilder<UserTruckEntity> builder)
        {
            builder.ToTable("UserTruck");

            builder.HasKey(c => new { c.UserId, c.TruckId });

            builder.HasOne(c => c.User)
                   .WithMany(c => c.UserTrucks)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Truck)
                   .WithMany(c => c.UserTrucks)
                   .HasForeignKey(c => c.TruckId)
                   .HasForeignKey(c => c.TruckId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
