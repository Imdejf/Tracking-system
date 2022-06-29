using TrackingSystem.Shared.Services.Interfaces.Base64FileValidator;

namespace TrackingSystem.Shared.Services.Implementations.Base64FileValidator
{
    internal sealed class Base64AnyFileValidator : AbstractBase64FileValidator<IBase64AnyFileValidatorValidationStage>,
        IBase64AnyFileValidatorValidationStage,
        IBase64AnyFileValidator
    {
    }
}
