using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingSystem.Domain.Entities.Events;

namespace JustCommerce.Persistence.DataAccess.EntitiesConfig.Event
{
    internal sealed class EventFileTypeConfig : IEntityTypeConfiguration<EventFileEntity>
    {
        public void Configure(EntityTypeBuilder<EventFileEntity> builder)
        {
            builder.ToTable("EventFile");

            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Event)
                   .WithMany(c => c.EventFiles)
                   .HasForeignKey(c => c.EventId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
