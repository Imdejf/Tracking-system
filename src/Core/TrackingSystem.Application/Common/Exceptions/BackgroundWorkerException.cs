using TrackingSystem.Shared.Abstract;

namespace TrackingSystem.Application.Common.Exceptions
{
    public sealed class BackgroundWorkerException : BaseAppException
    {
        public override int StatusCodeToRise => 400;
        public BackgroundWorkerException(string message) : base(message) { }
        public BackgroundWorkerException(Dictionary<string, string[]> errors) : base(errors) { }
    }
}
