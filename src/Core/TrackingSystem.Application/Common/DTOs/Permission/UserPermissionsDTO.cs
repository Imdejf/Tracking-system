namespace TrackingSystem.Application.Common.DTOs
{
    public class UserPermissionsDTO
    {
        public Guid UserId { get; set; }
        public ICollection<Tuple<PermissionDTO, bool>> Permission { get; set; }
    }
}
