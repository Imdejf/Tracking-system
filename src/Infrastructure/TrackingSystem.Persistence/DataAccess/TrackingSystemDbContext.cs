using JustCommerce.Persistence.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Repository;
using TrackingSystem.Domain.Entities.Abstract;
using TrackingSystem.Domain.Entities.Events;
using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Domain.Entities.Truck;
using TrackingSystem.Shared.Services.Interfaces;

namespace TrackingSystem.Persistence.DataAccess
{
    public sealed class TrackingSystemDbContext : DbContext, IUnitOfWork
    {
        private readonly ICurrentUserService _currentUserService;

        public TrackingSystemDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserPermissionEntity> UserPermission { get; set; }
        public DbSet<TruckDetailsEntity> _TruckDetails { get; set; }
        public DbSet<TruckEntity> _Trucks { get; set; }
        public DbSet<UserTruckEntity> _UserTrucks { get; set; }
        public DbSet<EventEntity> _Events { get; set; }

        public ITruckRepository Trucks => new TruckRepository(_Trucks, _UserTrucks);
        public ITruckDetailsRepository TruckDetails => new TruckDetailsRepository(_TruckDetails);
        public IEventRepository Event => new EventRepository(_Events);


        //Repository


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<Entity> entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.CurrentUser.Id.ToString();
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.CurrentUser.Id.ToString();
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
