namespace TrackingSystem.Infrastructure.Configurations
{
    public sealed class FtpFileConfig
    {
        public string Key { get; set; }
        public string Host { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UsingRootCatalog { get; set; }
        public bool EnableSSL { get; set; }
        public bool UsePassive { get; set; }
        public bool KeepAlive { get; set; }
        public decimal WaitForReadWriteOperationExecutionFor { get; set; }
        public int MaximumNumberOfFilesThatCanBeRemovedAtOnce { get; set; }
        public int WaitForRequestExecutionFor { get; set; }
        public string OnDomainName { get; set; }
    }
}
