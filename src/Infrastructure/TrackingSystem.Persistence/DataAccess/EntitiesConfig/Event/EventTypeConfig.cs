using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingSystem.Domain.Entities.Events;

namespace JustCommerce.Persistence.DataAccess.EntitiesConfig.Event
{
    internal sealed class EventTypeConfig : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.ToTable("Event");

            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id);

            builder.HasMany(c => c.EventFiles)
                   .WithOne(c => c.Event)
                   .HasForeignKey(c => c.EventId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.User)
                   .WithMany(c => c.Events)
                   .HasForeignKey(c => c.UserId);
        }
    }
}
