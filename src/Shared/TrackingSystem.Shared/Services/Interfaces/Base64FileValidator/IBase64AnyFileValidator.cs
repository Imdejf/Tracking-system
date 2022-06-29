namespace TrackingSystem.Shared.Services.Interfaces.Base64FileValidator
{
    public interface IBase64AnyFileValidator : IAbstractBase64FileValidatorFileStage<IBase64AnyFileValidatorValidationStage> { }
    public interface IBase64AnyFileValidatorValidationStage : IAbstractBase64FileValidatorValidationStage<IBase64AnyFileValidatorValidationStage>
    {

    }
}
