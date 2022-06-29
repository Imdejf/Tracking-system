namespace TrackingSystem.Shared.Models
{
    public sealed class PermissionObject
    {
        public string PermissionDomainName { get; set; }
        public string PermissionFlagName { get; set; }
        public int PermissionFlagValue { get; set; }
        internal PermissionObject(string domainName, string flagName, int flagValue)
        {
            PermissionDomainName = domainName;
            PermissionFlagName = flagName;
            PermissionFlagValue = flagValue;
        }
    }
}
