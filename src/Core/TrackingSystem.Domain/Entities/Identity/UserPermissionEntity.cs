namespace TrackingSystem.Domain.Entities.Identity
{
    public sealed class UserPermissionEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public string PermissionDomainName { get; set; }
        public int PermissionFlagValue { get; set; }
    }
}
