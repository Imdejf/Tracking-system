namespace TrackingSystem.Shared.Enums.Permissions
{
    [Flags]
    public enum TruckServicePermissions
    {
        AddUserToTruck = 1,
        RemoveUserFromTruck = 2,
        GetAllTruck = 4,
        GetTruckById = 8,
        GetTruckByUserId = 16,
    }
}
