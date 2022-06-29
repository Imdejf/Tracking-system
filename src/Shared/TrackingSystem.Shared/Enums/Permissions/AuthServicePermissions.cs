namespace TrackingSystem.Shared.Enums.Permissions
{
    [Flags]
    public enum AuthServicePermissions
    {
        CreateUser = 1,
        EditUser = 2,
        DeleteUser = 4,
        ViewManagementList = 8,
        ViewContactList = 16,
        ChangeOwnPassword = 32,
        ManagePermissions = 64,
        ChangeRole = 128,
        ChangePassword = 256,
        SetUserActiveOrDeactive = 512,
        RevokePermission = 1024,
        GrantPermission = 2048
    }
}
